using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEnemy : Enemy
{
    [SerializeField] private Transform projectileParent;
    public MagicEnemyData MagicEnemyData { get; protected set; }
    public Transform ProjectileParent { get; protected set; }

    protected override void Start()
    {
        base.Start();
        MagicEnemyData = enemyData as MagicEnemyData;
        ProjectileParent = projectileParent;
        EnemyAttackState = new EnemyShootState(this,StateMachine);
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 10);
    }
}
