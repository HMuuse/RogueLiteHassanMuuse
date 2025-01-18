using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentState;

    public void ChangeState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
