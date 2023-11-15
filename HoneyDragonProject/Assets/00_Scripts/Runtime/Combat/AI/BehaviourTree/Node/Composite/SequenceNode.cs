using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Composite/Sequence")]
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

            curINdex = 0;
            return NodeState.Success;
        }

        protected override void OnStart()
        {
        }

        protected override void OnEnd()
        {
        }
    }
}
