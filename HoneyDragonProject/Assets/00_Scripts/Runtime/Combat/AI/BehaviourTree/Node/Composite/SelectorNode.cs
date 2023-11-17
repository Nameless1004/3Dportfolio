using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Composite/Selector")]
    public class SelectorNode : CompositeNode
    {
        protected override void OnAwake()
        {
        }

        protected override void OnEnd()
        {
        }

        protected override void OnStart()
        {
        }

        protected override NodeState OnUpdate()
        {
            for(int i= 0; i < Children.Count; ++i)
            {
                var childState = Children[i].Evaluate();
                if (childState == NodeState.Success || childState == NodeState.Running)
                    return childState;
            }

            return NodeState.Failure; 
        }
    }
}
