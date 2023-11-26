using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [Description("자식 중 하나라도 성공하면 Success 반환\n모두 실패하면 Failure반환")]
    public class SelectorNode : Composite
    {
        protected override NodeState OnUpdate()
        {
            for (; currentChildIndex < Children.Count; currentChildIndex++)
            {
                if (Children[currentChildIndex] is Conditional) continue;

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
