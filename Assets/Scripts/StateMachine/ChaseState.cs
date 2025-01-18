using UnityEngine;

public class ChaseState : State
{
    private Vector3 directionToTarget = Vector3.zero;
    private float chaseSpeedMultiplier = 1.2f;

    public ChaseState(Entity entity, State parentState = null) : base(entity, parentState) { }

    public bool CanSeeTarget()
    {
        if (entity.target == null) return false;

        directionToTarget = entity.target.position - entity.transform.position;
        float distanceToTarget = directionToTarget.magnitude;

        // Check for target within detection range
        if (distanceToTarget <= entity.detectionRadius)
        {
            // Raycast for line of sight
            RaycastHit2D hit = Physics2D.Raycast(entity.transform.position, directionToTarget.normalized, distanceToTarget, entity.wallLayer);
            return hit.collider == null;  // No obstruction
        }

        return false;
    }

    public override void Enter()
    {
        Debug.Log("Entering ChaseState");
        entity.animator.Play("Run");
    }

    public override void Update()
    {
        base.Update();

        // Always update the direction to target in every frame
        if (CanSeeTarget())
        {
            entity.Chase(entity.target);
        }
        else
        {
            Debug.Log("Target lost, exiting ChaseState");
            entity.machine.ChangeState(new AIState(entity));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting ChaseState");
    }
}
