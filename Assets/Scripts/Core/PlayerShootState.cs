using UnityEngine;

public class PlayerShootState : PlayerState
{
    private float _shootDuration = 0.5f; // 射击状态的持续时间
    private float _timer;

    public PlayerShootState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        _timer = _shootDuration;
        player.Animator.SetTrigger("Shoot"); // 假设动画器中有个名为 "Shoot" 的触发器

        // 调用 PlayerShooter 的方法来实际发射子弹
        player.GetComponent<PlayerShooter>()?.Shoot();
    }

    public override void Update()
    {
        // 计时器递减
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            // 射击状态结束，返回待机状态
            stateMachine.ChangeState(player.IdleState);
        }
    }
}