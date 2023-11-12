using RPG.AI.BehaviourTree;
using RPG.Runtime.Combat.AI.BehaviourTree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public abstract class CompositeNode : NodeBase
    {
        public List<NodeBase> Children = new List<NodeBase>();
        public NodeBase ServiceNode;

        public override NodeState Evaluate(Blackboard blackboard)
        {
            UpdateServiceNode(blackboard);
            return base.Evaluate(blackboard);
        }

        private void UpdateServiceNode(Blackboard blackboard)
        {
            if(ServiceNode == null)
            {
                return;
            }

            ServiceNode.Evaluate(blackboard);
        }

        public override NodeBase Clone()
        {
            CompositeNode node = Instantiate(this);
            node.Children = Children.ConvertAll(x => x.Clone());

            if(ServiceNode!= null)
            {
                node.ServiceNode = ServiceNode.Clone();
            }
            return node;
        }
    }
}
