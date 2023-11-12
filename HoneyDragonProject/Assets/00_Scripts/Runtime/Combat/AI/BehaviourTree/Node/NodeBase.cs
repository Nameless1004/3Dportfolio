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
        public NodeState State = NodeState.Running;
        public bool Started = false;

        public virtual NodeState Evaluate(Blackboard blackboard)
        {
            if(Started == false)
            {
                OnStart(blackboard);
                Started = true;
            }

            State = OnUpdate(blackboard);

            if(State == NodeState.Success || State == NodeState.Failure)
            {
                Started = false;
                OnEnd(blackboard);
            }

            return State;
        }
        
        protected abstract void OnStart(Blackboard blackboard);
        protected abstract void OnEnd(Blackboard blackboard);
        // 노드 평가 함수
        protected abstract NodeState OnUpdate(Blackboard blackboard);

        public virtual NodeBase Clone()
        {
            return Instantiate(this);
        }
    }
}
