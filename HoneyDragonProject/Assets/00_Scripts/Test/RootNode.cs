public class RootNode : Node
{
    public RootNode(Node child) => this.child = child;

    private Node child;

    public override NodeState Evaluate()
    {
        return child.Evaluate();
    }
}
