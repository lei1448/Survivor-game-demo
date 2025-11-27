using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackVisual : MonoBehaviour
{
    [SerializeField] WeaponAttack rangeAttack;
    [SerializeField] private GameObject attackWeave;
    void Start()
    {
        attackWeave = Instantiate(attackWeave,rangeAttack.GetAttackPoint());
        attackWeave.SetActive(false);
        rangeAttack.OnRangeAttack += ActiveRangeAttack;
        rangeAttack.OnRangeAttackFinished += DeRangeAttack; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActiveRangeAttack(float range,Transform attackPoint)
    {
        Debug.Log("visual");
        attackWeave.SetActive (true);
        attackWeave.transform.position = attackPoint.transform.position + new Vector3(0,0.1f,0);
        attackWeave.transform.localScale = new Vector3(range, range, range) * 2;
    }
    private void DeRangeAttack()
    {
        attackWeave.SetActive(false);
    }
}
