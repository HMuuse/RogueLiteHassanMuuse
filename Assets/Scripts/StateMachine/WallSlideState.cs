using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : State
{
    private float wallSlidingSpeed = 2f;

    public WallSlideState(Entity entity) : base(entity) { }

    public override void Enter()
    {
        entity.animator.Play("WallJump");
    }

    public override void Update()
    {
        // Apply wall sliding velocity
        entity.rb.velocity = new Vector2(0, Mathf.Clamp(entity.rb.velocity.y, -wallSlidingSpeed, float.MaxValue));

        // Wall jump logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float wallJumpDirection = entity.IsFacingRight() ? -1 : 1;
            entity.rb.velocity = new Vector2(wallJumpDirection * entity.wallJumpForce, entity.jumpForce);
            entity.machine.ChangeState(new JumpState(entity));
        }

        // Exit wall slide if the entity moves away or lands
        if (!entity.IsWalled() || entity.IsGrounded())
        {
            entity.machine.ChangeState(new IdleState(entity));
        }
    }

    public override void Exit()
    {

    }
}
