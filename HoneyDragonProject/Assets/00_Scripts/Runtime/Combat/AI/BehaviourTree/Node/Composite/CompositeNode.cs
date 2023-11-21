using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public abstract class CompositeNode : NodeBase
    {
        public enum AbortType
        {
            None,
            Abort,
        }

        public List<NodeBase> Children = new List<NodeBase>();

        public List<NodeBase> conditionalNodeChildren;

        public bool raiseConditionAbort = false;
        public int currentChildIndex = 0;
        public AbortType abortType;

        public ConditionalNode abortedNode;

        public override void OnAwake()
        {
            conditionalNodeChildren = new List<NodeBase>();
            foreach (var i in Children)
            {
                i.Parent = this;
            }


            if (abortType == AbortType.Abort)
            {
                AddConditionalNode();
            }
        }

        private void AddConditionalNode()
        {
            Children.ForEach(x =>
            {
                if (x is ConditionalNode)
                {
                    conditionalNodeChildren.Add(x);
                }
                else if (x is DecoratorNode)
                {
                    var find = FindConditionalNode(x as DecoratorNode);
                    if (find != null)
                    {
                        conditionalNodeChildren.Add(find);
                    }
                }
            });
        }

        private ConditionalNode FindConditionalNode(DecoratorNode node)
        {
            if (node.Child is DecoratorNode)
            {
                return FindConditionalNode(node.Child as DecoratorNode);
            }

            if (node.Child is ConditionalNode)
            {
                return node.Child as ConditionalNode;
            }

            return null;
        }

       protected virtual ConditionalNode GetAbortedNode()
        {
            if (conditionalNodeChildren.Count == 0) return null;

            // abort 처리
            foreach (var condition in conditionalNodeChildren)
            {
                var conditionState = condition.Evaluate();
                if (conditionState == NodeState.Success)
                {
                    return condition as ConditionalNode;
                }
            }

            return null;
        }

        // -1이면 node가 자식에 없는 것
        public int GetChildIndex(NodeBase node)
        {
            for (int i = 0; i < Children.Count; ++i)
            {
                if (IsContains(node, Children[i], i) == true)
                {
                    return i;
                }
            }

            return -1;
        }

        private bool IsContains(NodeBase findNode, NodeBase compareNode, int index)
        {
            if (findNode == compareNode) return true;
            if (compareNode is DecoratorNode)
            {
                var deco = compareNode as DecoratorNode;
                return IsContains(findNode, deco.Child, index);
            }

            return false;
        }

        public override NodeBase Clone()
        {
            CompositeNode node = Instantiate(this);
            node.Children = Children.ConvertAll(x => x.Clone());
            node.abortType = abortType;
            return node;
        }

        public override void OnAbort()
        {
            if(abortedNode != null)
            {
                int index = GetChildIndex(abortedNode);
                currentChildIndex = index;
            }
        }

        public override NodeState Evaluate()
        {
            tree.currentExecutionNode = this;
            if (Started == false)
            {
                OnStart();
                Started = true;
            }

            if (Children.Count == 0) return State = NodeState.Failure;

            // 어보트된 노드를 가져온다.
            abortedNode = GetAbortedNode();
            // 중단된 노드의 인덱스로 이동
            if(abortedNode != null)
            {
                currentChildIndex = GetChildIndex(abortedNode);
            }

            State = OnUpdate();
            abortedNode = null;

            if (State == NodeState.Failure || State == NodeState.Success)
            {
                Started = false;
                OnEnd();
            }

            return State;
        }

        protected override void OnEnd()
        {
            if (State == NodeState.Failure || State == NodeState.Success)
            {
                currentChildIndex = 0;
            }
        }

    }
}
