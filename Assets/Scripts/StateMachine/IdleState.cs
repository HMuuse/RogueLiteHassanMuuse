using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Entity entity) : base(entity) { }

    public override void Enter()
    {

    }

    public override void Update()
    {
        float horizontalInput = entity.GetHorizontalInput();

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            entity.machine.ChangeState(new RunState(entity));
        }
        else if (entity.IsWalled() && !entity.IsGrounded() && entity.GetHorizontalInput() != 0f)
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
