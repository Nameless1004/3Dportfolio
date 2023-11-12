using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node.TestNode
{
    public class IsButtonClickNode : ActionNode
    {
        protected override void OnEnd(Blackboard blackboard)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnStart(Blackboard blackboard)
        {
            throw new System.NotImplementedException();
        }

        protected override NodeState OnUpdate(Blackboard blackboard)
        {
            if(Input.GetMouseButtonDown(0))
            {
                return NodeState.Success;
            }
            else return NodeState.Failure;
        }
    }
}
