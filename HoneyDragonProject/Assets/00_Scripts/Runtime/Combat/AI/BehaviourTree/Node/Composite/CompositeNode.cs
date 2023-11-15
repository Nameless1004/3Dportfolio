using System.Collections.Generic;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public abstract class CompositeNode : NodeBase
    {
        public List<NodeBase> Children = new List<NodeBase>();

        public override NodeBase Clone()
        {
            CompositeNode node = Instantiate(this);
            node.Children = Children.ConvertAll(x => x.Clone());

            return node;
        }
    }
}
