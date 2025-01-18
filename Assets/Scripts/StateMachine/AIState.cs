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
        if (entity.target != null && entity.machine.currentState is not ChaseState)
        {
            entity.machine.ChangeState(new ChaseState(entity, this));
        }

        else if (entity.IsGrounded())
        {
            if (entity.machine.currentState is not PatrolState)
            {
                entity.machine.ChangeState(new PatrolState(entity, this));
            }
        }
    }
}
