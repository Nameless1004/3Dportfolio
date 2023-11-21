using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Composite/Selector")]
    public class SelectorNode : CompositeNode
    {
        protected override NodeState OnUpdate()
        {
            for (; currentChildIndex < Children.Count; currentChildIndex++)
            {
                if(abortedNode != null && abortedNode == Children[currentChildIndex])
                {
                    abortedNode = null;
                    return NodeState.Success;
                }

                var state = Children[currentChildIndex].Evaluate();

                if (state == NodeState.Success || state == NodeState.Running)
                {
                    return state;
                }
            }

            return NodeState.Failure;
        }
    }
}
