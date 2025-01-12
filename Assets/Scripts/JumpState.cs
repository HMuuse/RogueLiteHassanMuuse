using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    public JumpState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        player.playerAnimator.SetBool("isJumping", true);
        player.Jump();
    }

    public override void Update()
    {
        if (player.IsGrounded())
        {
            player.machine.ChangeState(new IdleState(player));
        }
        else if (player.playerRigidbody.velocity.y < 0)
        {
            player.machine.ChangeState(new FallState(player));
        }
    }

    public override void Exit()
    {
        player.playerAnimator.SetBool("isJumping", false);
    }
}

