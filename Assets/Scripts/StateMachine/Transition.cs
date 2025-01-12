using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    public State TargetState { get; }
    public Func<bool> Condition { get; }

    public Transition(State targetState, Func<bool> condition)
    {
        TargetState = targetState;
        Condition = condition;
    }
}
