using UnityEngine;

public class PlayerHitState : PlayerState
{
    private float _hitDuration = .5f; // 受击无敌时间
    private float _timer;

    public PlayerHitState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        _timer = _hitDuration;
        player.Animator.SetTrigger("Hit"); // 假设动画器中有个名为 "Hit" 的触发器
        // 可以在这里添加屏幕闪红、音效等效果
        player.GetComponent<Collider>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;
    }

    public override void Update()
    {
        Vector3 movementDirection = new Vector3(player.MoveInput.x, 0, player.MoveInput.y);

        player.rb.velocity = movementDirection * player.GetPlayerMoveSpeed();

        // 让角色朝向移动的方向
        if(movementDirection != Vector3.zero)
        {
            player.transform.rotation = Quaternion.LookRotation(movementDirection);
        }
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            // 受击状态结束，返回待机状态

            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        player.GetComponent<Collider>().enabled = true;
        player.GetComponent<Rigidbody>().useGravity = true;
    }
}