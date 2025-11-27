using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPartrolState : EnemyState
{
    private Collider[] detectedColliders = new Collider[10];
    private float attackColdTime;
    public EnemyPartrolState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        enemy.Anim.SetBool("Partrol",true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Anim.SetBool("Partrol", false);
    }

    public override void Update()
    {
        moveDir = enemy.GetMovDir();
        enemy.SetVelocity(moveDir * enemy.MoveSpeed);
        attackColdTime -= Time.deltaTime;
        if(Physics.OverlapSphereNonAlloc(enemy.transform.position, enemy.DetectedRadius, detectedColliders, 1 << 6) > 0 && attackColdTime <= 0)
        {
            attackColdTime = 3;
            stateMachine.ChangeState(enemy.EnemyReadyForAttackState);
        }
    }
}
