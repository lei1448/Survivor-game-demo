using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon : Weapon
{
    protected Enemy targetEnemy;
   
    protected Collider[] colliders;
    protected float minDistance = 999;
    protected float targetDistance;

    protected virtual void FindNearestEnemy()
    {
        colliders = Physics.OverlapSphere(transform.position, realRange,enemyLayerMask);
        if(colliders.Length <= 0 ) targetEnemy = null;
        foreach(Collider c in colliders)
        {
            targetDistance = Vector3.Distance(c.transform.position,transform.position);
            if(targetDistance < minDistance)
            {
                minDistance = targetDistance;
                targetEnemy = c.GetComponent<Enemy>();
            }
        }
        if(targetEnemy != null)
            transform.LookAt(targetEnemy.transform.position);
        minDistance = 999;
    }
}
