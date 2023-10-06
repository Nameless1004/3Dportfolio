using System.Collections.Generic;

public class SequenceNode : CompositeNode
{
    public SequenceNode(List<Node> children) => this.children = children;

    public override NodeState Evaluate()
    {
        foreach(var child in children)
        {
            var childResult = child.Evaluate();
            if(childResult == NodeState.Failure || childResult == NodeState.Running )
            {
                return childResult;
            }
        }

        return NodeState.Success;
    }
}
