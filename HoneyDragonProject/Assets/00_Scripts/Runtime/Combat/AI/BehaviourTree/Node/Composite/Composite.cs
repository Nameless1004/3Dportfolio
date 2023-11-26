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
    public abstract class Composite : NodeBase
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
                if (x is Conditional)
                {
                    conditionalNodeChildren.Add(x);
                }
                else if (x is Decorator)
                {
                    var find = FindConditionalNode(x as Decorator);
                    if (find != null)
                    {
                        conditionalNodeChildren.Add(find);
                    }
                }
            });
        }

        private Conditional FindConditionalNode(Decorator node)
        {
            if (node.Child is Decorator)
            {
                return FindConditionalNode(node.Child as Decorator);
            }

            if (node.Child is Conditional)
            {
                return node.Child as Conditional;
            }

            return null;
        }

       protected virtual Conditional GetAbortedNode()
        {
            if (conditionalNodeChildren.Count == 0) return null;

            // abort 처리
            foreach (var condition in conditionalNodeChildren)
            {
                var conditionState = condition.Evaluate();
                if (conditionState == NodeState.Success)
                {
                    return condition as Conditional;
                }
            }

            return null;
        }

        // -1이면 node가 자식에 없는 것
        protected virtual int GetChildIndex(NodeBase node)
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

        protected bool IsContains(NodeBase findNode, NodeBase compareNode, int index)
        {
            if (findNode == compareNode) return true;
            if (compareNode is Decorator)
            {
                var deco = compareNode as Decorator;
                return IsContains(findNode, deco.Child, index);
            }

            return false;
        }

        public override NodeBase Clone()
        {
            Composite node = Instantiate(this);
            node.Children = Children.ConvertAll(x => x.Clone());
            node.abortType = abortType;
            return node;
        }

        public override void OnAbort()
        {
            Children[currentChildIndex].OnAbort();
            OnEnd();
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
            var abortedNode = GetAbortedNode();
            
            // 중단
            if(abortedNode != null)
            {
                State = NodeState.Failure;
                OnAbort();
                return State;
            }

            State = OnUpdate();

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
