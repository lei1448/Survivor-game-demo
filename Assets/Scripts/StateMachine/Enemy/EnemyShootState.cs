using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShootState : EnemyAttackState
{
    protected MagicEnemy magicEnemy;
    public EnemyShootState(MagicEnemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        this.magicEnemy = enemy;
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        GameObject newProjectile = GameServices.Get<ObjectPoolManager>().GetFromPool(magicEnemy.MagicEnemyData.projectilePrefab, magicEnemy.transform.position, Quaternion.identity, magicEnemy.ProjectileParent);
        newProjectile.GetComponent<Projectile>().SetProjectile(magicEnemy.MagicEnemyData.projectileLifeTime, magicEnemy.MagicEnemyData.damage, magicEnemy.MagicEnemyData.moveSpeed * magicEnemy.MagicEnemyData.projectileSpeed * playerDir);
        stateMachine.ChangeState(magicEnemy.PartrolState);
    }
}
