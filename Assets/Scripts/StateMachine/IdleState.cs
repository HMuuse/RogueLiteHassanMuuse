using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Entity entity) : base(entity) { }

    public override void Enter()
    {
        entity.animator.Play("Idle");
    }

    public override void Update()
    {
        float horizontalInput = 0f;

        if (entity is PlayerController player)
        {
            // Get input specifically for the player
            horizontalInput = player.GetHorizontalInput();
        }

        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(entity.rb.velocity.x) > 0.1f)
        {
            entity.machine.ChangeState(new RunState(entity));
        }
        else if (entity.IsWalled() && !entity.IsGrounded() && horizontalInput != 0f)
        {
            entity.machine.ChangeState(new WallSlideState(entity));
        }
        else if (Input.GetKeyDown(KeyCode.Space) && entity.IsGrounded())
        {
            Debug.Log("transitiong to jump state");
            entity.machine.ChangeState(new JumpState(entity));
        }
    }
}
