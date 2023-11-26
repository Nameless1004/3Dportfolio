using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [Description("자식노드가 Failure나 Running을 반환하면 재평가합니다")]
    public class ReactiveSequenceNode : Composite
    {
        protected override NodeState OnUpdate()
        {
            for (; currentChildIndex < Children.Count; currentChildIndex++)
            {
                if (Children[currentChildIndex] is Conditional) continue;

                var state = Children[currentChildIndex].Evaluate();

                if (state == NodeState.Running || state == NodeState.Failure)
                {
                    currentChildIndex = 0;
                    return state;
                }
            }
            return NodeState.Success;
        }
    }
}
