using UnityEngine;

public class DamagePopupGenerator : MonoBehaviour
{
    public static DamagePopupGenerator Instance
    {
        get; private set;
    }

    [SerializeField]
    private ObjectPool damagePopupPool; 
    [SerializeField]
    private Transform canvasTransform;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Create(Vector3 position, int damageAmount)
    {
        GameObject popupObject = damagePopupPool.GetFromPool();
        popupObject.transform.position = position;

        DamagePopup damagePopup = popupObject.GetComponent<DamagePopup>();
        if(damagePopup != null)
        {
            // 关键：将对象池的引用传递给弹出的数字
            damagePopup.PoolToReturnTo = this.damagePopupPool;
            damagePopup.Setup(damageAmount);
        }
    }
}