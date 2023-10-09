using System;
using System.Collections.Generic;

public class SequenceNode : CompositeNode
{
    public SequenceNode(List<Node> children) => this.children = children;
    private Node runningNode= null;


    public override NodeState Evaluate()
    {
        if(runningNode != null)
        {
            if(IsConditionChanged(runningNode))
            {
                runningNode.Abort();
                // 실행중인 노드를 중단 후 첫 번째 노드부터 재평가
                return EvaluateChildren(0);

            }
            var runningNodeIndex = children.IndexOf(runningNode);
            var curState = runningNode.Evaluate();
            if(curState == NodeState.Success)
            {
                return EvaluateChildren(runningNodeIndex + 1);
            }

            return HandleState(curState, runningNode);
            
        }
        return EvaluateChildren(0);
    }

    private bool IsConditionChanged(Node node)
    {
        var runningNodeIndex = children.IndexOf(node);
        for(int i = 0; i < runningNodeIndex; i++)
        {
            var candiate = children[i];
            if(candiate.CanUpdate() == false)
            {
                return true;
            }
        }

        return false;
    }

    private NodeState EvaluateChildren(int index)
    {
        for(int i = index; i < children.Count; i++)
        {
            var child = children[i];
            var childResult = child.Evaluate();

            if (childResult == NodeState.Success) continue;
            return state = HandleState(childResult, child);
        }


        return state = HandleState(NodeState.Success, null);
    }

    public NodeState HandleState(NodeState state, Node node)
    {
        runningNode = state == NodeState.Running ? node : null;
        return state;
    }
}
