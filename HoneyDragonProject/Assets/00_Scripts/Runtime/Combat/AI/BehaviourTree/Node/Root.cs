using UnityEditor.EditorTools;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [Description("Entry Node")]
    public class Root : NodeBase
    {
        public NodeBase Child;

        protected override NodeState OnUpdate()
        {
            return Child.Evaluate();
        }

        public override NodeBase Clone()
        {
            Root root = Instantiate(this);
            root.Child = Child.Clone();
            return root;
        }

    }
}
