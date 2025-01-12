using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public RunState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        player.playerAnimator.SetBool("isRunning", true);
    }

    public override void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalInput) < 0.1f)
        {
            player.machine.ChangeState(new IdleState(player));
        }
        else if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded())
        {
            player.machine.ChangeState(new JumpState(player));
        }
        else
        {
            player.Move(horizontalInput);
        }
    }
}

