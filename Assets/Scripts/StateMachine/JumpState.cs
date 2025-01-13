using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    public JumpState(Entity entity) : base(entity) { }

    public override void Enter()
    {
        entity.animator.Play("Jump");
        entity.Jump();
    }

    public override void Update()
    {
        float horizontalInput = 0f;

        if (entity is PlayerController player)
        {
            // Get input specifically for the player
            horizontalInput = player.GetHorizontalInput();
        }

        if (entity.IsGrounded() && entity.rb.velocity.y <= 0)
        {
            entity.machine.ChangeState(new IdleState(entity));
        }
        else if (entity.IsWalled() && !entity.IsGrounded() && horizontalInput != 0f)
        {
            entity.machine.ChangeState(new WallSlideState(entity));
        }
        else if (entity.CanDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            entity.machine.ChangeState(new DoubleJumpState(entity));
        }
        else if (entity.rb.velocity.y < 0)
        {
            entity.machine.ChangeState(new FallState(entity));
        }

        entity.Move(horizontalInput);
    }

    public override void Exit()
    {

    }
}
