using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : State
{
    public AIState (Entity entity) : base(entity) { }

    public override void Enter()
    {
        Debug.Log("Entering AIState");
    }

    public override void Update()
    {
        if (entity.IsGrounded())
        {
            if (entity.machine.currentState is not PatrolState)
            {
                entity.machine.ChangeState(new PatrolState(entity, this));
            }
        }
        else if (entity.IsWalled())
        {
            entity.machine.ChangeState(new AvoidObstacleState(entity, this));
        }
    }
}
