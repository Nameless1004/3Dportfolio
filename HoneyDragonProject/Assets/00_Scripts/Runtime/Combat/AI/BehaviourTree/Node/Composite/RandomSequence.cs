using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class RandomSequenceNode : Composite
    {
        public List<NodeBase> shuffledChildren;

        public override void OnAwake()
        {
            base.OnAwake();
            shuffledChildren = Children.ToList();
        }

        protected override void OnStart()
        {
            Util.Utility.Shuffle(shuffledChildren);
        }

        protected override NodeState OnUpdate()
        {
            for (; currentChildIndex < shuffledChildren.Count; currentChildIndex++)
            {
                if (Children[currentChildIndex] is Conditional) continue;

                var state = shuffledChildren[currentChildIndex].Evaluate();

                if (state == NodeState.Running || state == NodeState.Failure)
                {
                    return state;
                }
            }
            return NodeState.Success;

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
