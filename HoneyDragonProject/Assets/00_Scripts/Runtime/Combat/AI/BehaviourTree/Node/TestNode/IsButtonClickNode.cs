using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node.TestNode
{
    public class IsButtonClickNode : ActionNode
    {
        public override NodeState Evaluate(Blackboard blackboard)
        {
            if(Input.GetMouseButtonDown(0))
            {
                return NodeState.Success;
            }
            else return NodeState.Failure;
        }
    }
}
