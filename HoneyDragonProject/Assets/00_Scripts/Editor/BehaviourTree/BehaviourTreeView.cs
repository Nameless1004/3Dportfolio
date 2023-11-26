using RPG.Combat.AI.BehaviourTree;
using RPG.Combat.AI.BehaviourTree.Node;
using RPG.Core.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;

public class BehaviourTreeView : GraphView
{
    public Action<NodeView> OnNodeSelected;
    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits> { }
    public BehaviourTree tree;
    public BehaviourTreeView()
    {
        Insert(0, new GridBackground());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/00_Scripts/Editor/BehaviourTree/BehaviourTreeEditor.uss");
        styleSheets.Add(stylesheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void OnUndoRedo()
    {
        PopulateView(tree);
        AssetDatabase.SaveAssets();
    }

    NodeView FindNodeView(NodeBase node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }

    public void PopulateView(BehaviourTree tree)
    {
        this.tree = tree;
        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (tree.root == null)
        {
            tree.root = tree.CreateNode(typeof(Root)) as Root;
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }

        // Create node View
        tree.nodes.ForEach(n => CreateNodeView(n));

        // Create edges
        tree.nodes.ForEach(n =>
        {
            var children = tree.GetChildren(n);
            children.ForEach(child =>
            {
                NodeView parentView = FindNodeView(n);
                NodeView childView = FindNodeView(child);

                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            });
        });
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort =>
        endPort.direction != startPort.direction &&
        endPort.node != startPort.node).ToList();
    }

    
    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null)
                {
                    tree.DeleteNode(nodeView.node);
                }

                Edge edge = elem as Edge;
                if (edge != null)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    tree.RemoveChild(parentView.node, childView.node);
                }
            });
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                tree.AddChild(parentView.node, childView.node);
            });
        }

        if (graphViewChange.movedElements != null)
        {
            nodes.ForEach(node =>
            {
                NodeView view = node as NodeView;
                view.SortChildren();
            });
        }
        return graphViewChange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        Vector2 mousePosition = GUIUtility.GUIToScreenPoint(evt.mousePosition);

        {
            var types = TypeCache.GetTypesDerivedFrom<RPG.Combat.AI.BehaviourTree.Node.Action>();
            foreach (var type in types)
            {
                string typeName = type.Name;
                evt.menu.AppendAction($"{type.BaseType.Name}/{typeName}", (a) => CreateNode(type, mousePosition));
            }
        }

        {
            var types = TypeCache.GetTypesDerivedFrom<Composite>();
            foreach (var type in types)
            {
                string typeName = type.Name;
                evt.menu.AppendAction($"{type.BaseType.Name}/{typeName}", (a) => CreateNode(type, mousePosition));
            }
        }

        {
            var types = TypeCache.GetTypesDerivedFrom<Decorator>();
            foreach (var type in types)
            {
                string typeName = type.Name;
                evt.menu.AppendAction($"{type.BaseType.Name}/{typeName}", (a) => CreateNode(type, mousePosition));
            }
        }

        {
            var types = TypeCache.GetTypesDerivedFrom<Root>();
            foreach (var type in types)
            {
                string typeName = type.Name;
                evt.menu.AppendAction($"{type.BaseType.Name}/{typeName}", (a) => CreateNode(type, mousePosition));
            }
        }

        {
            var types = TypeCache.GetTypesDerivedFrom<Conditional>();
            foreach (var type in types)
            {
                string typeName = type.Name;
                evt.menu.AppendAction($"{type.BaseType.Name}/{typeName}", (a) => CreateNode(type, mousePosition));
            }
        }
    }

    void CreateNode(Type type)
    {
        NodeBase node = tree.CreateNode(type);
        CreateNodeView(node);
    }

    void CreateNode(Type type, Vector2 screenMousePosition)
    {
        NodeBase node = tree.CreateNode(type, screenMousePosition);
        CreateNodeView(node, screenMousePosition);
    }

    void CreateNodeView(NodeBase node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected += OnNodeSelected;
        AddElement(nodeView);
    }

    void CreateNodeView(NodeBase node, Vector2 screenMousePosition)
    {
        BehaviourTreeEditor editorWindow = BehaviourTreeEditor.Instance;
        var windowMousePosition = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, screenMousePosition - editorWindow.position.position);
        var graphMousePosition = editorWindow.treeView.contentViewContainer.WorldToLocal(windowMousePosition);
        var nodeOffset = new Vector2(-75, -20);
        var nodePosition = graphMousePosition + nodeOffset;
        NodeView nodeView = new NodeView(node, nodePosition);
        nodeView.OnNodeSelected += OnNodeSelected;
        AddElement(nodeView);
    }

    public void UpdateNodeState()
    {
        nodes.ForEach(n =>
        {
            NodeView view = n as NodeView;
            view.UpdateState();
        });

    }
}
