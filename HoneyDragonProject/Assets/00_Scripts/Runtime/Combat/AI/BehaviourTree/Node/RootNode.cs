using RPG.AI.BehaviourTree;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class RootNode : NodeBase
    {
        public RootNode(NodeBase node)
        {
            this.node = node;
        }

        private NodeBase node;

        public override NodeState Evaluate(Blackboard blackboard)
        {
            return node.Evaluate(blackboard);
        }
    }
}
