using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        // 进入行走状态时，设置动画参数
        player.Animator.SetBool("IsWalking", true);
    }

    public override void Update()
    {
        // 在行走状态下，持续检测是否还有移动输入
        // 如果没有移动输入，则切换回待机状态
        if(player.MoveInput == Vector2.zero)
        {
            stateMachine.ChangeState(player.IdleState);
            return; // 提前返回，避免执行下面的移动逻辑
        }

        // 计算移动向量
        Vector3 movementDirection = new Vector3(player.MoveInput.x, 0, player.MoveInput.y);

        player.rb.velocity = movementDirection * player.GetPlayerMoveSpeed() + new Vector3(0,player.rb.velocity.y, 0);

        // 让角色朝向移动的方向
        if(movementDirection != Vector3.zero)
        {
            player.transform.rotation = Quaternion.LookRotation(movementDirection);
        }
    }
}