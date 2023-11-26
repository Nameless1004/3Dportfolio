using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [Description("모든 자식이 성공하면 Success\n하나라도 실패한다면 Failure 반환")]
    public class SequenceNode : Composite
    {
        protected override NodeState OnUpdate()
        {
            for (; currentChildIndex < Children.Count; currentChildIndex++)
            {
                if (Children[currentChildIndex] is Conditional) continue;

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
