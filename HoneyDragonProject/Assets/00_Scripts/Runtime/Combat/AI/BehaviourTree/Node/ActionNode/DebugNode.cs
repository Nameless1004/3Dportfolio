using RPG.Combat.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Action/Debug")]
    public class DebugNode : ActionNode
    {
        public string message;
        protected override NodeState OnUpdate()
        {
            Debug.Log(message);
            return NodeState.Success;
        }

        protected override void OnStart()
        {
        }

        protected override void OnEnd()
        {
        }

        protected override void OnAwake()
        {
        }
    }
}
