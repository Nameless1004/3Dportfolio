public class BehaviourTree
{
    public BehaviourTree(RootNode node)
    {
        root = node;
    }

    Node root;
    public void UpdateTree()
    {
        if (root != null)
        {
            root.Evaluate();
        }
    }
}
