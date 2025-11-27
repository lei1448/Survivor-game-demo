using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReadyForAttackState : EnemyState
{
    protected float attackLeadTime = 0.2f;

    public EnemyReadyForAttackState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Anim.SetBool("Ready", true);
       // enemy.SetVelocity(Vector3.zero);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Anim.SetBool("Ready", false);
    }

    public override void Update()
    {
        base.Update();
        if(attackLeadTime <= stateTimer)
        {
            stateMachine.ChangeState(enemy.EnemyAttackState);
        }

    }
}
