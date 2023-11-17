using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class SequenceNode : CompositeNode
    {
        private int curINdex = 0;

        protected override NodeState OnUpdate()
        {
            for (; curINdex  < Children.Count; ++curINdex)
            {
                var state = Children[curINdex].Evaluate();
                if (state == NodeState.Failure || state == NodeState.Running)
                    return state;
            }

            return NodeState.Success;
        }

        protected override void OnAwake()
        {
        }

        protected override void OnStart()
        {
        }

        protected override void OnEnd()
        {
            if (State == NodeState.Failure) 
            {
                curINdex = 0;
            }
        }

    }
}
