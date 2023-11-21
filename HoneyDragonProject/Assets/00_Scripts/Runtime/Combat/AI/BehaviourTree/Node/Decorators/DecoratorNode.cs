using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public abstract class DecoratorNode : NodeBase
    {
        [HideInInspector] public NodeBase Child;

        public override void OnAwake()
        {
            if(Child != null)
            {
                Child.Parent = this;
            }
        }

        public override NodeBase Clone()
        {
            DecoratorNode node = Instantiate(this);
            node.Child = Child.Clone();

            return node;
        }

        public override void OnAbort()
        {
            State = NodeState.Failure;
            if (Child != null )
            {
                Child.OnAbort();
            }
        }
    }
}
