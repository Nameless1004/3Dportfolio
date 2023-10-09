using System;

public abstract class ConditionalNode : Node
{
    protected Node child;
    public Node Child { get { return child; } set { child = value; } }
    private bool isRunning = false;
    public override NodeState Evaluate()
    {
        // 자식이 없으면 리프 노드이다.
       if(child == null)
        {
            state = CanUpdate() ? NodeState.Success : NodeState.Failure;
            return state;
        }

       if(CanUpdate())
        {
            state = child.Evaluate();
            isRunning = state == NodeState.Running && IsUpdatable();
            return state;
        }

        return state = NodeState.Failure;
    }

    public override void Abort()
    {
        if(isRunning)
        {
            isRunning = false;
            child?.Abort();
        }
    }

    protected abstract bool IsUpdatable();

}
