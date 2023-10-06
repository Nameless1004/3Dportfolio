using System;

public class IfNode : DecoratorNode
{
    public IfNode(Func<bool> condition, Node child)
    {
        this.condition = condition;
        this.child = child;
    }
    private Func<bool> condition;
    private Node child;

    public override NodeState Evaluate()
    {
        if(condition.Invoke())
        {
            return child.Evaluate();
        }
        else
        {
            return NodeState.Failure;
        }
    }
}
