using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.SetPlayerVelocity(Vector3.zero);
        // 进入待机状态时，设置动画参数
        player.Animator.SetBool("IsWalking", false);
    }

    public override void Update()
    {
        // 在待机状态下，持续检测移动输入
        // 如果有移动输入，则切换到行走状态
        if(player.MoveInput != Vector2.zero)
        {
            stateMachine.ChangeState(player.WalkState);
        }
    }
}