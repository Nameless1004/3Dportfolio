using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Decorator/RepeatUntilFail")]
    public class RepeatUntilFailNode : DecoratorNode
    {
        protected override void OnEnd(Blackboard blackboard)
        {

        }

        protected override void OnStart(Blackboard blackboard)
        {

        }

        protected override NodeState OnUpdate(Blackboard blackboard)
        {
            return NodeState.Success;
        }
    }
}
