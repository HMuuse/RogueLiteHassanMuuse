using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Entity entity;
    protected State parentState;

    public State(Entity entity, State parentState = null)
    {
        this.entity = entity;
        this.parentState = parentState;
    }

    public virtual void Enter()
    {
        parentState?.Enter();
    }
    public virtual void Update()
    {
        parentState?.Update();
    }
    public virtual void Exit()
    {
        parentState?.Exit();
    }

    public virtual void OverrideState()
    {

    }
}
