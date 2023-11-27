using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public abstract class Decorator : NodeBase
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
            Decorator node = Instantiate(this);
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
