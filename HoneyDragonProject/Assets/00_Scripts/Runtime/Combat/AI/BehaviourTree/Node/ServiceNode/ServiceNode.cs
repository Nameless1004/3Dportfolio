using RPG.AI.BehaviourTree;
using RPG.Combat.AI.BehaviourTree.Node;
using System;

namespace RPG.Runtime.Combat.AI.BehaviourTree.Node
{
    public abstract class ServiceNode : INode
    {
        public abstract void Update(Blackboard blackboard);
    }
}
