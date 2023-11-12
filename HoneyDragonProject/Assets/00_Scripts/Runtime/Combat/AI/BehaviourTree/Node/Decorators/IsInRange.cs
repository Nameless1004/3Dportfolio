using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class IsInRange : DecoratorNode
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
            if((GameObject)blackboard.GetData("Target") == null)
            {
                return NodeState.Failure;
            }

            return Child.Evaluate(blackboard);
        }
    }
}
