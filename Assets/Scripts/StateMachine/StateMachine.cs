using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentState;

    public void ChangeState(State newState)
    {
        Debug.Log($"Changing state from {currentState?.GetType().Name} to {newState.GetType().Name}");
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
