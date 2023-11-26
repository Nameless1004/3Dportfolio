using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [Description("실패할때까지 재평가")]
    public class RepeatUntilFail : Decorator
    {

        protected override NodeState OnUpdate()
        {
            if(NodeState.Failure == Child.Evaluate())
            {
                return NodeState.Success;
            }

            return NodeState.Running;
        }
    }
}
