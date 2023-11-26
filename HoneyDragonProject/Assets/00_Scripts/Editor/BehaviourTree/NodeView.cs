using RPG.Combat.AI.BehaviourTree;
using RPG.Combat.AI.BehaviourTree.Node;
using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public NodeBase node;
    public Port input;
    public Port output;
    public Label descriptionLabel;
    public NodeView(NodeBase node) : base("Assets/00_Scripts/Editor/BehaviourTree/NodeView.uxml")
    {
        var fold = mainContainer.Q<Foldout>();
        var desc = (DescriptionAttribute)node.GetType().GetCustomAttribute(typeof(DescriptionAttribute));
        if(desc != null )
        {
            fold.SetEnabled(true);
            fold.value = false;
            fold.contentContainer.Add(new Label(desc.Description));
        }
        else
        {
            fold.SetEnabled(false);
        }
        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.guid;
        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();
    }

    public NodeView(NodeBase node, Vector2 pos) : base("Assets/00_Scripts/Editor/BehaviourTree/NodeView.uxml")
    {
        this.capabilities &= ~(Capabilities.Snappable);
        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.guid;
        style.left = pos.x;
        style.top = pos.y;

        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();
    }

    private void SetupClasses()
    {
        if (node is RPG.Combat.AI.BehaviourTree.Node.Action)
        {
            AddToClassList("action");
        }
        else if (node is Composite)
        {
            AddToClassList("composite");
        }
        else if (node is Decorator)
        {
            AddToClassList("decorator");
        }
        else if (node is Root)
        {
            AddToClassList("root");
        }
        else if (node is Conditional)
        {
            AddToClassList("condition");
        }
    }

    private void CreateInputPorts()
    {
        if (node is RPG.Combat.AI.BehaviourTree.Node.Action)
        {
            input = base.InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if (node is Conditional)
        {
            input = base.InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if (node is Composite)
        {
            input = base.InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if(node is Decorator)
        {
            input = base.InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if (node is Root)
        {
           
        }

        if(input != null)
        {
            input.portName = "";
            input.style.flexDirection = FlexDirection.Column;
            input.style.width = 100;
            inputContainer.Add(input);
        }
    }

    private void CreateOutputPorts()
    {
        if (node is RPG.Combat.AI.BehaviourTree.Node.Action)
        {
        }
        else if (node is Composite)
        {
            output = base.InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if (node is Decorator)
        {
            output = base.InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }
        else if (node is Root)
        {
            output = base.InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }

        if (output != null)
        {
            output.portName = "";
            output.style.flexDirection = FlexDirection.ColumnReverse;
            output.style.width = 40;
            outputContainer.Add(output);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Undo.RecordObject(node, "Behaviour Tree (Set Position)");
        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
        EditorUtility.SetDirty(node);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        if(OnNodeSelected != null)
        {
            OnNodeSelected.Invoke(this);
        }
    }

    public void SortChildren()
    {
        Composite composite = node as Composite;
        if(composite)
        {
            composite.Children.Sort(SortByHorizontalPosition);
        }
    }

    private int SortByHorizontalPosition(NodeBase x, NodeBase y)
    {
        return x.position.x < y.position.x ? -1 : 1;
    }

    public void UpdateState()
    {
        RemoveFromClassList("success");
        RemoveFromClassList("failure");
        RemoveFromClassList("running");
        
        if (Application.isPlaying)
        {
            if (node is Conditional) return;
            switch (node.State)
            {
                case NodeState.Success:
                    AddToClassList("success");
                    break;
                case NodeState.Failure:
                    AddToClassList("failure");
                    break;
                case NodeState.Running:
                    if (node.Started)
                    {
                        AddToClassList("running");
                    }
                    break;
            }
        }
    }
}
