using System.Collections.Generic;

public class SelectorNode : CompositeNode
{
    public SelectorNode(List<Node> children)
    {
        this.children = children;
    }

    public override NodeState Evaluate()
    {
        foreach(var child in children)
        {
            var childResult = child.Evaluate();
            if (childResult == NodeState.Success || childResult == NodeState.Running)
                return childResult;
        }

        return NodeState.Failure;
    }
}
