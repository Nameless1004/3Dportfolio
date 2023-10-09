using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Success,
    Failure,
    Running
}

public abstract class Node
{
    protected NodeState state = NodeState.Failure;
    protected bool isStarted = false;

    public virtual void OnStart() { }
    public abstract NodeState Evaluate();
    public virtual void OnEnd() { }

    // 조건부 중단
    public virtual void Abort() { }

    public virtual bool CanUpdate()
    {
        return true;
    }
}


public abstract class ActionNode : Node
{
}

