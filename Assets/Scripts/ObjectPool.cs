using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("池对象预制体")]
    [SerializeField] private GameObject objectPrefab;

    [Header("池对象父物体")]
    [SerializeField] private Transform objectParent;

    [Header("池大小")]
    [SerializeField] private int poolSize;

    private int activeCount = 0;//激活对象数量

    // 使用队列作为池的容器，先进先出
    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        // 游戏开始时，预先创建对象并填充池
        PrewarmPool();
    }

    // 预热池，创建初始数量的对象
    private void PrewarmPool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    // 创建一个新对象并将其放入池中
    private void CreateNewObject()
    {
        GameObject newObj = Instantiate(objectPrefab,objectParent);
        newObj.SetActive(false); // 初始状态为非激活
        pool.Enqueue(newObj);    // 入队，放入池中
    }

    // 从池中获取一个对象
    public GameObject GetFromPool()
    {
        // 如果池中没有可用对象，动态创建一个新的
        if(pool.Count == 0)
        {
            Debug.LogWarning("池已空，正在创建新对象。考虑增大池大小。");
            CreateNewObject();
        }

        // 从池中取出一个对象
        GameObject objToGet = pool.Dequeue();
        objToGet.SetActive(true);
        activeCount++;
        return objToGet;
    }

    // 将一个对象返回到池中
    public void ReturnToPool(GameObject objToReturn)
    {
        objToReturn.SetActive(false);
        activeCount--;
        pool.Enqueue(objToReturn);
    }

    public int GetActiveCounter()
    {
        return activeCount;
    }
}