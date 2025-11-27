using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon
{

    [Header("Events")]
    [SerializeField] protected VoidEvent onAttackFinished;
    [SerializeField] protected Transform_FloatEvent OnRangeAttack;
    protected override void Start()
    {
        base.Start();
        attackPoint = player.transform;
    }
    protected override void Attack()
    {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0)
        {
            DamageEnemy();
            attackTimer = currentAttackColdTime / realSpeed;
        }
        else if(attackTimer <= (currentAttackColdTime / realSpeed) * 0.8)
        {
            onAttackFinished?.Raise(new Void { });
        }
    }

    protected override void DamageEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(attackPoint.position, realRange);
        foreach(Collider enemyCol in enemies)
        {
            if(enemyCol.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(realDamage,realKnockForce);
            }
        }
        OnRangeAttack?.Raise(new Transform_Float { transform = attackPoint, value = realRange });
    }

}
