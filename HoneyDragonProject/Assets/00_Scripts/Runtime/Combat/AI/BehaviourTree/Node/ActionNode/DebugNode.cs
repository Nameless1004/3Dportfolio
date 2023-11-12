using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Action/Debug")]
    public class DebugNode : ActionNode
    {
        public string message;
        protected override NodeState OnUpdate(Blackboard blackboard)
        {
            Debug.Log(message);
            return NodeState.Success;
        }

        protected override void OnStart(Blackboard blackboard)
        {
        }

        protected override void OnEnd(Blackboard blackboard)
        {
        }
    }
}
