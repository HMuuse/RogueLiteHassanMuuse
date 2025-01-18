using UnityEngine;

public class PatrolState : State
{
    private float patrolDirection = 1f;

    public PatrolState(Entity entity, State parentState = null) : base(entity, parentState){ }

    public override void Enter()
    {
        Debug.Log("Entering PatrolState");
        entity.animator.Play("Run");
    }

    public override void Update()
    {
        base.Update();
        entity.Move(patrolDirection);

        // Change patrol direction if NPC hits a wall or edge
        if (entity.IsWalled())
        {
            patrolDirection *= -1;
        }

        // Example condition to transition back to AIState
        if (!entity.IsGrounded())
        {
            entity.machine.ChangeState(new AIState(entity));
        }
    }
}

