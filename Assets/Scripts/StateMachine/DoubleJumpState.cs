using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpState : State
{
    public DoubleJumpState(Entity entity) : base(entity) { }

    public override void Enter()
    {
        entity.animator.Play("DoubleJump");
        entity.Jump();
    }

    public override void Update()
    {
        if (entity.rb.velocity.y < 0)
        {
            entity.machine.ChangeState(new FallState(entity));
        }
    }
}


