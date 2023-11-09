using RPG.AI.BehaviourTree;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class RepeatUntilSuccess : DecoratorNode
    {
        public RepeatUntilSuccess(NodeBase child) 
        {
            Child = child;
        }

        public override NodeState Evaluate(Blackboard blackboard)
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
