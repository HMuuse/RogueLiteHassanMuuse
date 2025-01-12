using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class FallState : State
{
    public FallState(Entity entity) : base(entity) { }

    public override void Enter()
    {
        entity.animator.SetBool("isFalling", true);
    }

    public override void Update()
    {
        float horizontalInput = entity.GetHorizontalInput();

        if (entity.IsWalled() && !entity.IsGrounded())
        {
            entity.machine.ChangeState(new WallSlideState(entity));
        }
        else if (entity.IsGrounded())
        {
            Debug.Log("Transitioning to IdleState from FallState");
            // Transition to IdleState
            entity.machine.ChangeState(new IdleState(entity));
        }

        entity.Move(horizontalInput);
    }


    public override void Exit()
    {
        entity.animator.SetBool("isFalling", false);
    }
}

