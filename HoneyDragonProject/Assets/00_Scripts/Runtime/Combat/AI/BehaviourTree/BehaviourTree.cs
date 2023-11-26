using RPG.Combat.AI.BehaviourTree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree
{
    public abstract class BehaviourTree : ScriptableObject
    {
        public Root root;
        public GameObject owner;
        protected Blackboard blackboard;
        public Blackboard Blackboard { get { return blackboard; } }
        public List<NodeBase> nodes = new List<NodeBase>();

        public NodeBase currentExecutionNode;
        public NodeState BehaviourTreeState = NodeState.Running;


        protected abstract void CreateBlackboard(GameObject owner);

        public NodeState TreeUpdate()
        {
            if (BehaviourTreeState == NodeState.Running)
            {
                BehaviourTreeState = root.Evaluate();
            }
            else
            {
                BehaviourTreeState = NodeState.Failure;
            }
            return BehaviourTreeState;
        }

        public void Traverse(NodeBase node, Action<NodeBase> visiter)
        {
            if (node)
            {
                visiter.Invoke(node);
                var children = GetChildren(node);
                children.ForEach((n) => Traverse(n, visiter));
            }
        }

        public BehaviourTree Clone()
        {
            BehaviourTree tree = Instantiate(this);
            tree.root = (Root)root.Clone();
            tree.nodes = new List<NodeBase>();
            Traverse(tree.root, (n) =>
            {
                tree.nodes.Add(n);
                n.name = n.GetType().Name;
                n.tree = this;
                n.OnAwake();
            });

            return tree;
        }

        // 오너 게임오브젝트도 등록
        public void Bind(GameObject owner)
        {
            if (blackboard == null)
            {
                CreateBlackboard(owner);
            }

            Traverse(root, node =>
            {
                node.blackboard = blackboard;
            });

        }

        //#if UNITY_EDITOR
        public NodeBase CreateNode(Type type, Vector2 pos)
        {
            NodeBase node = ScriptableObject.CreateInstance(type) as NodeBase;
            node.name = type.Name.Replace("Node","");
            node.guid = GUID.Generate().ToString();
            node.position = pos;

            Undo.RecordObject(this, "Behaviour Tree(CreateNode)");
            nodes.Add(node);

            if (!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(node, this);
            }
            Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree(CreateNode)");
            AssetDatabase.SaveAssets();
            return node;

        }

        public NodeBase CreateNode(Type type)
        {
            NodeBase node = ScriptableObject.CreateInstance(type) as NodeBase;
            node.name = type.Name.Replace("Node", "");
            node.guid = GUID.Generate().ToString();

            Undo.RecordObject(this, "Behaviour Tree(CreateNode)");
            nodes.Add(node);

            if (!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(node, this);
            }
            Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree(CreateNode)");
            AssetDatabase.SaveAssets();
            return node;

        }

        public void DeleteNode(NodeBase node)
        {
            Undo.RecordObject(this, "Behaviour Tree(DeleteNode)");
            nodes.Remove(node);

            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(NodeBase parent, NodeBase child)
        {
            Decorator decorator = parent as Decorator;
            if (decorator)
            {
                Undo.RecordObject(decorator, "Behaviour Tree(AddChild)");
                decorator.Child = child;
                EditorUtility.SetDirty(decorator);
            }

            Root root = parent as Root;
            if (root)
            {
                Undo.RecordObject(root, "Behaviour Tree(AddChild)");
                root.Child = child;
                EditorUtility.SetDirty(root);
            }

            Composite composite = parent as Composite;
            if (composite)
            {
                Undo.RecordObject(composite, "Behaviour Tree(AddChild)");
                composite.Children.Add(child);
                EditorUtility.SetDirty(composite);
            }
        }

        public void RemoveChild(NodeBase parent, NodeBase child)
        {
            child.Parent = null;
            Decorator decorator = parent as Decorator;
            if (decorator)
            {
                Undo.RecordObject(decorator, "Behaviour Tree(RemoveChild)");
                decorator.Child = null;
                EditorUtility.SetDirty(decorator);
            }

            Root root = parent as Root;
            if (root)
            {
                Undo.RecordObject(root, "Behaviour Tree(RemoveChild)");
                root.Child = null;
                EditorUtility.SetDirty(root);
            }


            Composite composite = parent as Composite;
            if (composite)
            {
                Undo.RecordObject(composite, "Behaviour Tree(RemoveChild)");
                composite.Children.Remove(child);
                EditorUtility.SetDirty(composite);
            }
        }

        //#endif

        public List<NodeBase> GetChildren(NodeBase parent)
        {
            List<NodeBase> children = new List<NodeBase>();

            Decorator decorator = parent as Decorator;
            if (decorator && decorator.Child != null)
            {
                children.Add(decorator.Child);
            }

            Root root = parent as Root;
            if (root && root.Child != null)
            {
                children.Add(root.Child);
            }

            Composite composite = parent as Composite;
            if (composite && composite.Children != null)
            {
                children.AddRange(composite.Children.ToList());
            }
            return children;
        }

    }
}
