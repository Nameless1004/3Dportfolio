using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [Description("성공할때까지 재평가")]
    public class RepeatUntilSuccess : Decorator
    {

        protected override NodeState OnUpdate()
        {
            if(Child.Evaluate() == NodeState.Success)
            {
                return NodeState.Success;
            }

            return NodeState.Running;
        }
    }
}
