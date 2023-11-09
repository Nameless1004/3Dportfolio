using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class DebugNode : ActionNode
    {
        public DebugNode(string message) { this.message = message; }
        public string message;
        public override NodeState Evaluate(Blackboard blackboard)
        {
            Debug.Log(message);
            return NodeState.Success;
        }
    }
}
