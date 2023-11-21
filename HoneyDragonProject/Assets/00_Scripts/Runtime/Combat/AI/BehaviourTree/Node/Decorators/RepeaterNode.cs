using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName ="BehaviourTree/Node/Decorator/Repeater")]
    public class RepeaterNode : DecoratorNode
    {
        public int RepeatCount;
        public bool RunForever;

        private int repeatCount;
        public RepeaterNode(NodeBase repeatedNode) 
        {
            Child = repeatedNode;
        }
        protected override void OnStart()
        {
            repeatCount = RepeatCount;
        }
        protected override void OnEnd()
        {
            repeatCount = 0;
        }
        protected override NodeState OnUpdate()
        {
            if(RunForever)
            {
                Child.Evaluate();
                return NodeState.Running;
            }

            Child.Evaluate();
            repeatCount--;
            if(repeatCount == 0)
            {
                return NodeState.Success;
            }
            else
            {
                return NodeState.Running;
            }
        }
    }
}
