using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [Description("지정한 횟수만큼 자식노드를 반복합니다.")]
    public class Repeater : Decorator
    {
        public int RepeatCount;
        public bool RunForever;

        private int repeatCount;
        public Repeater(NodeBase repeatedNode) 
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
