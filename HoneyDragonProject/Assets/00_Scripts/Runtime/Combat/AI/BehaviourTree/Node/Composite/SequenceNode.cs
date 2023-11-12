using RPG.AI.BehaviourTree;
using RPG.Runtime.Combat.AI.BehaviourTree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Composite/Sequence")]
    public class SequenceNode : CompositeNode
    {
        public SequenceNode(List<NodeBase> children, ServiceNode serviceNode = null)
        {
            this.Children = children;
            this.ServiceNode = serviceNode;
        }

        private int curINdex = 0;
        protected override NodeState OnUpdate(Blackboard blackboard)
        {
            ServiceNode?.Evaluate(blackboard);

            for (; curINdex  < Children.Count; ++curINdex)
            {
                var state = Children[curINdex].Evaluate(blackboard);
                if (state == NodeState.Failure || state == NodeState.Running)
                    return state;
            }

            curINdex = 0;
            return NodeState.Success;
        }

        protected override void OnStart(Blackboard blackboard)
        {
        }

        protected override void OnEnd(Blackboard blackboard)
        {
        }
    }
}
