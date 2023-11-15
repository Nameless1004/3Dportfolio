using Cysharp.Threading.Tasks;
using RPG.AI.BehaviourTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public enum NodeState
    {
        Success,
        Failure,
        Running
    }

    public interface INode { }

    [CreateAssetMenu(fileName = "Node", menuName = "Node/"), Serializable]
    public abstract class NodeBase : ScriptableObject, INode
    {
        [HideInInspector] public NodeState State = NodeState.Running;
        [HideInInspector] public bool Started = false;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        [HideInInspector] public Blackboard blackboard;
        public BehaviourTree tree;

        public virtual NodeState Evaluate()
        {
            tree.currentExecutionNode = this;
            if (Started == false)
            {
                OnStart();
                Started = true;
            }

            State = OnUpdate();

            if(State == NodeState.Success || State == NodeState.Failure)
            {
                Started = false;
                OnEnd();
            }

            return State;
        }
        
        protected abstract void OnStart();
        protected abstract void OnEnd();
        // 노드 평가 함수
        protected abstract NodeState OnUpdate();

        public virtual NodeBase Clone()
        {
            return Instantiate(this);
        }
    }
}
