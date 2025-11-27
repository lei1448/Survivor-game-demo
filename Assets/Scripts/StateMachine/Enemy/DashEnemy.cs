using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        EnemyAttackState = new EnemyDashState(this,StateMachine);
    }
}
