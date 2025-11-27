using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPartrolState : EnemyState
{
    private Vector3 moveDir;

    public EnemyPartrolState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        moveDir = enemy.GetMovDir();
        enemy.SetVelocity(moveDir * enemy.moveSpeed);
        if(Vector3.Distance(enemy.transform.position, Player.Instance.transform.position) < enemy.attackDistance)
        {
            stateMachine.ChangeState(enemy.EnemyAttackState);
        }
    }
}
