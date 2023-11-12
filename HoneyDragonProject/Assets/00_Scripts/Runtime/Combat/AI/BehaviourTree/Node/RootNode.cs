using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName ="BehaviourTree/Node/Root")]
    public class RootNode : NodeBase
    {
        public NodeBase Child;

        protected override NodeState OnUpdate(Blackboard blackboard)
        {
            return Child.Evaluate(blackboard);
        }

        protected override void OnStart(Blackboard blackboard)
        {
        }

        protected override void OnEnd(Blackboard blackboard)
        {
        }

        public override NodeBase Clone()
        {
            RootNode root = Instantiate(this);
            root.Child = Child.Clone();
            return root;
        }
    }
}
