using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Decorator/RepeatUntilFail")]
    public class RepeatUntilFailNode : DecoratorNode
    {
        protected override void OnEnd()
        {

        }

        protected override void OnStart()
        {

        }

        protected override NodeState OnUpdate()
        {
            return NodeState.Success;
        }
    }
}
