using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class RandomSelectorNode : CompositeNode
    {
        public List<NodeBase> shuffledChildren;

        public override void OnAwake()
        {
            shuffledChildren = Children.ToList();
        }

        protected override void OnStart()
        {
            Util.Utility.Shuffle(shuffledChildren);
        }

        // �ڽĳ�� �� �ϳ��� success, running�� ��ȯ�ϸ� �θ𿡰� ��ȯ
        protected override NodeState OnUpdate()
        {
            if (Children.Count == 0) return NodeState.Failure;


            for (; currentChildIndex < shuffledChildren.Count; currentChildIndex++)
            {
                if (abortedNode != null && abortedNode == shuffledChildren[currentChildIndex])
                {
                    abortedNode = null;
                    return NodeState.Success;
                }

                var state = shuffledChildren[currentChildIndex].Evaluate();

                if (state == NodeState.Success || state == NodeState.Running)
                {
                    return state;
                }
            }

            return NodeState.Failure;
        }

        protected override int GetChildIndex(NodeBase node)
        {
            for (int i = 0; i < shuffledChildren.Count; ++i)
            {
                if (IsContains(node, shuffledChildren[i], i) == true)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
