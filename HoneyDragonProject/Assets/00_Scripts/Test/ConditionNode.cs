using System;

public class ConditionNode : Node
{
    public ConditionNode(Func<bool> condition) => this.condition = condition;
    protected Func<bool> condition;
    public override NodeState Evaluate()
    {
        if(condition.Invoke())
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }

}
