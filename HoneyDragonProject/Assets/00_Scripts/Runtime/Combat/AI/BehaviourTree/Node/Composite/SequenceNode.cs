using RPG.AI.BehaviourTree;
using RPG.Runtime.Combat.AI.BehaviourTree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class SequenceNode : CompositeNode
    {
        public SequenceNode(List<NodeBase> children, ServiceNode serviceNode = null)
        {
            this.children = children;
            this.serviceNode = serviceNode;
        }

        private int curINdex = 0;
        public override NodeState Evaluate(Blackboard blackboard)
        {
            serviceNode?.Update(blackboard);

            for (; curINdex  < children.Count; ++curINdex)
            {
                var state = children[curINdex].Evaluate(blackboard);
                if (state == NodeState.Failure || state == NodeState.Running)
                    return state;
            }

            curINdex = 0;
            return NodeState.Success;
        }
    }
}
