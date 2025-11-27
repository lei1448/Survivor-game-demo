using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{

    private void Awake()
    {
        GameServices.Register<ObjectPoolManager>(this);
    }

    private void OnDestroy()
    {
        GameServices.UnRegister(this);
    }
    private readonly Dictionary<GameObject, Queue<GameObject>> pools = new();
    public GameObject GetFromPool(GameObject prefab,Vector3 position,Quaternion rotation,Transform parent)
    {
        if(!pools.ContainsKey(prefab))
        {
            pools[prefab] = new Queue<GameObject>(); 
        }

        GameObject instance;

        if(pools.ContainsKey(prefab) && pools[prefab].Count > 0)
        {
            instance = pools[prefab].Dequeue();
            instance.transform.SetPositionAndRotation(position, rotation);
            instance.SetActive(true);
        }
        else
        {   
            instance = Instantiate(prefab,position,rotation,parent);
            var prefabInfo = instance.AddComponent<PooledObjectInfo>();
            prefabInfo.OriginalPrefab = prefab;
        }
        return instance;
    }

    public void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);
        if(instance.TryGetComponent<PooledObjectInfo>(out var prefabInfo))
        {
            pools[prefabInfo.OriginalPrefab].Enqueue(instance);
        }
        else
        {
            Debug.LogWarning($"对象 {instance.name} 不是由池创建的，将被销毁。");
            Destroy(instance);
        }
    }
}

