using NUnit.Framework;
using System.Collections.Generic;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public abstract class CompositeNode : NodeBase
    {
        public List<NodeBase> children;
    }
}
