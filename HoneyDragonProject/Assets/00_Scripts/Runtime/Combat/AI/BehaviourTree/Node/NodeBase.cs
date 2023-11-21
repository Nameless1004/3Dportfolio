using System;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public enum NodeState
    {
        None,
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
        public NodeBase Parent = null;

        public virtual NodeState Evaluate()
        {
            tree.currentExecutionNode = this;
            if (Started == false)
            {
                OnStart();
                Started = true;
            }

            State = OnUpdate();

            if (State == NodeState.Success || State == NodeState.Failure)
            {
                Started = false;
                OnEnd();
            }

            return State;
        }

        /// <summary>
        /// 한 번만 실행되는 초기화 함수입니다.
        /// </summary>
        public virtual void OnAwake() { }

        /// <summary>
        /// OnEnd() 호출을 했다면 다음 노드를 실행할 때 OnStart가 호출됩니다
        /// </summary>
        protected virtual void OnStart(){}
        protected virtual void OnEnd() { }

        public virtual void OnAbort()
        {
            OnEnd();
        }

        // 노드 평가 함수
        protected abstract NodeState OnUpdate();

        public virtual NodeBase Clone()
        {
            return Instantiate(this);
        }
    }
}
