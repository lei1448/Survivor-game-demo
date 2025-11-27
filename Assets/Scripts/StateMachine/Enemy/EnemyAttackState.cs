using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected Vector3 playerDir;
    
    public EnemyAttackState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerDir = enemy.GetMovDir();
        enemy.Anim.SetBool("Attack",true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Anim.SetBool("Attack",false);
    }

    public override void Update()
    {
        base.Update();
    }
}
