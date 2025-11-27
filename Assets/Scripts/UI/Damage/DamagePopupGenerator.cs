using UnityEngine;

public class DamagePopupGenerator : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager damagePopupPool; 
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Transform parent;
    public void Create(Transform_Float pos_dam)
    {
        GameObject popupObject = damagePopupPool.GetFromPool(popupPrefab,pos_dam.transform.position,popupPrefab.transform.rotation,parent);

        DamagePopup damagePopup = popupObject.GetComponent<DamagePopup>();
        if(damagePopup != null)
        {
            damagePopup.Setup(pos_dam.value);
        }
    }
}