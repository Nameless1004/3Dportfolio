using RPG.AI.BehaviourTree;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName ="BehaviourTree/Node/Root")]
    public class RootNode : NodeBase
    {
        public NodeBase Child;

        protected override NodeState OnUpdate()
        {
            return Child.Evaluate();
        }

        protected override void OnStart()
        {
        }

        protected override void OnEnd()
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
