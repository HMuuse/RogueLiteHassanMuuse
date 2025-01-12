using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        player.playerAnimator.SetBool("isRunning", false);
    }

    public override void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            player.machine.ChangeState(new RunState(player));
        }
        else if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded())
        {
            player.machine.ChangeState(new JumpState(player));
        }
    }
}

