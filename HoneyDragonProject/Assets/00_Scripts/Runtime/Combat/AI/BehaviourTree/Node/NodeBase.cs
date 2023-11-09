using RPG.AI.BehaviourTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public enum NodeState
    {
        Success,
        Failure,
        Running
    }

    public interface INode { }

    public abstract class NodeBase : INode
    {
        // 노드 평가 함수
        public abstract NodeState Evaluate(Blackboard blackboard);
    }
}
