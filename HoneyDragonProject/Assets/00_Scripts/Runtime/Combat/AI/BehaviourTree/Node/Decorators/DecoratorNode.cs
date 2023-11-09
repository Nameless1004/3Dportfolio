using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public abstract class DecoratorNode : NodeBase
    {
        public NodeBase Child;
    }
}
