using UnityEngine;

public class AvoidObstacleState : State
{
    private float avoidDuration = 1f; // Time to spend in this state
    private float elapsedTime = 0f;

    public AvoidObstacleState(Entity entity, State parentState = null) : base(entity, parentState) { }

    public override void Enter()
    {
        Debug.Log("Entering AvoidObstacleState");
        entity.Move(entity.IsFacingRight() ? -1f : 1f); // Move away from the obstacle
        elapsedTime = 0f; // Reset timer
    }

    public override void Update()
    {
        base.Update();

        elapsedTime += Time.deltaTime;

        // Remain in this state for the specified duration
        if (elapsedTime >= avoidDuration && !entity.IsWalled())
        {
            Debug.Log("AvoidObstacleState: Transitioning to PatrolState");
            entity.machine.ChangeState(new PatrolState(entity, parentState));
        }
    }
}
