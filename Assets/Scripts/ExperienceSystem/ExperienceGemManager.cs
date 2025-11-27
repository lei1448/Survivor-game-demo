using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGemManager : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager gemPool;
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private Transform parent;
    public void DropExperienceGem(Transform_Float pos_val)
    {
        GameObject gem = gemPool.GetFromPool(gemPrefab,pos_val.transform.position,Quaternion.identity,parent);
        gem.GetComponent<ExperienceGem>().SetExperienceValue((int)pos_val.value);
    }
}
