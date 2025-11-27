using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    private float backTime = 0.1f;//±»»÷ÍËÊ±¼ä
    public EnemyHitState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Anim.SetBool("Hit",true);
        enemy.SetVelocity(2 * enemy.BackForce * -enemy.GetMovDir());
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer > backTime)
        {
            stateMachine.ChangeState(enemy.PartrolState);
        }
    }

    public override void Exit()
    {
        enemy.Anim.SetBool("Hit", false);
    }
}
