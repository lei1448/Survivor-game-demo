using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private Vector3 offset;
    protected override void Attack()
    {
        attackPoint = player.transform;
        AroundPlayer();
        transform.LookAt(attackPoint, Vector3.up);
    }

    private void AroundPlayer()
    {
        Quaternion turn = Quaternion.Euler(0, realSpeed * 50 * Time.deltaTime, 0);
        transform.position = player.transform.position + offset;
        offset = turn * offset;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(realDamage,realKnockForce);
        }
    }
    public override void InitializeWeaponRealData()
    {
        base.InitializeWeaponRealData();
        offset = transform.position - player.transform.position;
        offset.Normalize();
        offset = offset * realRange;
    }
}
