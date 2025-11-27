using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiedState : PlayerState
{
    public PlayerDiedState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Animator.SetBool("playerDied",true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Animator.SetBool("playerDied",false);
    }

    public override void Update()
    {
        base.Update();
    }
}
