using RPG.Combat.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Action/Debug")]
    public class Debug : Action
    {
        public string message;
        protected override NodeState OnUpdate()
        {
            UnityEngine.Debug.Log(message);
            return NodeState.Success;
        }

        protected override void OnStart()
        {
        }

        protected override void OnEnd()
        {
        }
    }
}
