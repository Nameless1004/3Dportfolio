using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class SequenceNode : CompositeNode
    {
        protected override NodeState OnUpdate()
        {
            for (; currentChildIndex < Children.Count; currentChildIndex++)
            {
                if (abortedNode == Children[currentChildIndex])
                {
                    abortedNode.State = NodeState.Success;
                    abortedNode = null;
                    continue;
                }

                var state = Children[currentChildIndex].Evaluate();

                if (state == NodeState.Running || state == NodeState.Failure)
                {
                    return state;
                }
            }
            return NodeState.Success;

        }

    }
}
