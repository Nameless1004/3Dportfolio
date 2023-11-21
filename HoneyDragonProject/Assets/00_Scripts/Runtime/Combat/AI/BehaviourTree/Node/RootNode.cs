namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class RootNode : NodeBase
    {
        public NodeBase Child;

        protected override NodeState OnUpdate()
        {
            return Child.Evaluate();
        }

        public override NodeBase Clone()
        {
            RootNode root = Instantiate(this);
            root.Child = Child.Clone();
            return root;
        }

    }
}
