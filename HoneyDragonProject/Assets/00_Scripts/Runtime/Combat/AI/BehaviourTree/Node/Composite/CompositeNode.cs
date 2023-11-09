using RPG.AI.BehaviourTree;
using RPG.Runtime.Combat.AI.BehaviourTree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public abstract class CompositeNode : NodeBase
    {
        protected List<NodeBase> children;
        protected ServiceNode serviceNode;
    }
}
