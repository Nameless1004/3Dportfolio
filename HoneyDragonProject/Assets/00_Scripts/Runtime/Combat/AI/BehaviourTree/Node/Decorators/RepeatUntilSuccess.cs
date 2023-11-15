using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Decorator/RepeatUntilSuccess")]
    public class RepeatUntilSuccess : DecoratorNode
    {

        protected override void OnEnd()
        {
      
        }

        protected override void OnStart()
        {

        }

        protected override NodeState OnUpdate()
        {
            var state = Child.Evaluate();

            if(state == NodeState.Success)
            {
                return state;
            }

            return NodeState.Running;
        }
    }
}
