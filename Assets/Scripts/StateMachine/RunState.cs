using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public RunState(Entity entity) : base(entity) { }

    public override void Enter()
    {
        entity.animator.Play("Run");
    }

    public override void Update()
    {
        float horizontalInput = 0f;

        if (entity is PlayerController player)
        {
            // Get input specifically for the player
            horizontalInput = player.GetHorizontalInput();
        }

        if (Mathf.Abs(horizontalInput) < 0.1f)
        {
            entity.machine.ChangeState(new IdleState(entity));
        }
        else if (Input.GetKeyDown(KeyCode.Space) && entity.IsGrounded())
        {
            entity.machine.ChangeState(new JumpState(entity));
        }
        else if (entity.IsWalled() && !entity.IsGrounded() && horizontalInput != 0f)
        {
            entity.machine.ChangeState(new WallSlideState(entity));
        }
        else
        {
            entity.Move(horizontalInput);
        }
    }

    public override void Exit()
    {
    }
}