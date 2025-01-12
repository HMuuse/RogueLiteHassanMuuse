using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : State
{
    public FallState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        player.playerAnimator.SetBool("isFalling", true);
    }

    public override void Update()
    {
        if (player.IsGrounded())
        {
            player.machine.ChangeState(new IdleState(player));
        }
    }

    public override void Exit()
    {
        player.playerAnimator.SetBool("isFalling", false);
    }
}

