using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Decorator/RepeatUntilSuccess")]
    public class RepeatUntilSuccess : DecoratorNode
    {

        protected override void OnEnd(Blackboard blackboard)
        {
      
        }

        protected override void OnStart(Blackboard blackboard)
        {

        }

        protected override NodeState OnUpdate(Blackboard blackboard)
        {
            var state = Child.Evaluate(blackboard);

            if(state == NodeState.Success)
            {
                return state;
            }

            return NodeState.Running;
        }
    }
}
