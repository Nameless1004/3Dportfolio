using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;
using static Unity.VisualScripting.Metadata;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CustomEditor(typeof(BehaviourTree))]
    public class TreeEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            var s = target as BehaviourTree;
            if(s.root != null)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("root"));
                NodeBase node = s.root.Child;
                if(node != null)
                {
                    ShowPropertyNode(null, node);
                }
            }
        }

        public void ShowPropertyNode(NodeBase parent, NodeBase node)
        {
            EditorGUILayout.Space(30);
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleLeft;
            style.normal.textColor = Color.red;
            style.fontSize = 14;

            if (node is CompositeNode)
            {
                CompositeNode comp = node as CompositeNode;
                
                EditorGUILayout.TextField(node.GetType().Name, style);
                EditorGUILayout.ObjectField(node, node.GetType());
                EditorGUILayout.Space(10);
                EditorGUILayout.PropertyField(new SerializedObject(comp).FindProperty("Children"));
            }
            else if (node is DecoratorNode)
            {
                DecoratorNode deco = node as DecoratorNode;
          
                EditorGUILayout.TextField(node.GetType().Name, style);
                EditorGUILayout.LabelField(deco.GetType().Name);
                EditorGUILayout.PropertyField(new SerializedObject(node).FindProperty("Child"));
                ShowPropertyNode(node, deco.Child);
            }
            else
            {
                EditorGUILayout.TextField(node.GetType().BaseType.Name, style);
                EditorGUILayout.ObjectField(node, node.GetType());
            }
        }
    }
}
