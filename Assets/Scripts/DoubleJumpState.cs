using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpState : State
{
    public DoubleJumpState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        player.playerAnimator.SetTrigger("doubleJump");
        player.Jump();
    }

    public override void Update()
    {
        if (player.playerRigidbody.velocity.y < 0)
        {
            player.machine.ChangeState(new FallState(player));
        }
    }
}

