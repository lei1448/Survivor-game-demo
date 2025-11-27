using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDashState : EnemyAttackState
{
    private float dashTime = 0.5f;
    private float dashSpeed;
    public EnemyDashState(DashEnemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.ToggleCanBeKnocked();
        dashSpeed = enemy.MoveSpeed * 7;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.ToggleCanBeKnocked();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(playerDir * dashSpeed);
        if(stateTimer > dashTime)
        {
            stateMachine.ChangeState(enemy.PartrolState);
        }
    }
}
