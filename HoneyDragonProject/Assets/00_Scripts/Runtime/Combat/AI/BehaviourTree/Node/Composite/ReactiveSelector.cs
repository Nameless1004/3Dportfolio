using RPG.Combat.AI.BehaviourTree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AI.BehaviourTree.Node
{
    public class ReactiveSelector : Composite
    {
        protected override NodeState OnUpdate()
        {
            for (; currentChildIndex < Children.Count; currentChildIndex++)
            {
                if (Children[currentChildIndex] is Conditional) continue;

                var state = Children[currentChildIndex].Evaluate();

                if (state == NodeState.Success || state == NodeState.Running)
                {
                    currentChildIndex = 0;
                    return state;
                }
            }

            return NodeState.Failure;
        }
    }
}
