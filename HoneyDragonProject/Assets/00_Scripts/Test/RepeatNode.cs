public class RepeatNode : ConditionalNode
{
    public RepeatNode(int loopCount, Node child)
    {
        this.repeatCount = loopCount;
        this.child = child;
    }

    int currentCount = 0;
    int repeatCount = 0;

    public override NodeState Evaluate()
    {
        for(currentCount = 0;  currentCount < repeatCount; currentCount++)
        {
            var childResult = child.Evaluate();
            if(childResult == NodeState.Failure || childResult == NodeState.Running)
            {
                return childResult;
            }
        }

        return NodeState.Success;
    }

    protected override bool IsUpdatable()
    {
        return true;
    }
}
