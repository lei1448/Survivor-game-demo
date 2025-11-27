using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float attackTimer;
    private float attackColdTime = 0.1f;

    public EnemyAttackState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        enemy.SetVelocity(Vector3.zero);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        if(Vector3.Distance(enemy.transform.position, Player.Instance.transform.position) > enemy.attackDistance)
        {
            stateMachine.ChangeState(enemy.PartrolState);
        }
        AttackPlayer();  
    }
    private void AttackPlayer()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer > attackColdTime)
        {
            Collider[] player;
            player = Physics.OverlapSphere(enemy.transform.position, enemy.GetAttackRange(), enemy.GetPlayerLayerMask());
            if(player.Length > 0 && player[0].TryGetComponent<Player>(out Player playerCmp))
            {
                playerCmp.TakeDamage(enemy.GetAttackDamage());
            }
            attackTimer = 0;
        }
    }

}
