using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.Animator.SetBool("IsIdle",true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Animator.SetBool("IsIdle",false);
    }

    public override void Update()
    {
        base.Update();
        if(player.MoveInput != Vector2.zero)
        {
            stateMachine.ChangeState(player.WalkState);
            return;
        }
        player.Rb.velocity = new Vector3(0f, player.Rb.velocity.y, 0f);
    }

    
}