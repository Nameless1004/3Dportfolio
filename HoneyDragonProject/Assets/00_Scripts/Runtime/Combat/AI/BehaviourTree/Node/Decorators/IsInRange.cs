using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class IsInRange : DecoratorNode
    {
        public IsInRange(NodeBase child)
        {
            Child = child;
        }
        public override NodeState Evaluate(Blackboard blackboard)
        {
            if((GameObject)blackboard.GetData("Target") == null)
            {
                return NodeState.Failure;
            }

            return Child.Evaluate(blackboard);
        }
    }
}
