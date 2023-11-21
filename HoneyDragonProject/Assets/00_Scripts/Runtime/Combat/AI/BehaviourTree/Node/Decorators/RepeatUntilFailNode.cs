using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Decorator/RepeatUntilFail")]
    public class RepeatUntilFailNode : DecoratorNode
    {

        protected override NodeState OnUpdate()
        {
            return NodeState.Success;
        }
    }
}
