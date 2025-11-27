using System.Collections;
using UnityEngine;

public class PlayerHitState : PlayerState
{
    private float _hitDuration = .5f;

    public PlayerHitState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.Animator.SetBool("Hit",true);
        player.ToggleInvincible();
    }

    public override void Update()
    {
        base.Update();
        Vector3 movementDirection = new Vector3(player.MoveInput.x, 0, player.MoveInput.y);

        player.Rb.velocity = movementDirection * player.MoveSpeed;

        if(movementDirection != Vector3.zero)
        {
            player.transform.rotation = Quaternion.LookRotation(movementDirection);
        }

        if(stateTimer >= _hitDuration)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        player.Animator.SetBool("Hit",false);
        player.ToggleInvincible();
    }

}