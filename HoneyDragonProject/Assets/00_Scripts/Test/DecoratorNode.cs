public abstract class DecoratorNode : Node
{
    protected Node child;
    public void SetChild(Node child) => this.child = child;
}