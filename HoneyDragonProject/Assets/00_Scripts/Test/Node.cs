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
    protected NodeState state = NodeState.Running;

    public abstract NodeState Evaluate();
}


public class ActionNode : Node
{
    public ActionNode(Action action) => this.action = action;
    protected Action action;
    public override NodeState Evaluate()
    {
        action?.Invoke();
        return NodeState.Success;
    }
}

