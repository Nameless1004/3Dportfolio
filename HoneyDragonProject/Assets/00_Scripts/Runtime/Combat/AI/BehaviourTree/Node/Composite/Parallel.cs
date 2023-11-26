using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class Parallel : Composite
    {
        private List<NodeState> childStates;
        private bool isRunning;
        public override void OnAwake()
        {
            childStates = new List<NodeState>();
            for (int i = 0; i < Children.Count; ++i)
            {
                childStates.Add(NodeState.Running);
            }
        }

        protected override void OnStart()
        {
            currentChildIndex = 0;
            for (int i = 0; i < childStates.Count; ++i)
            {
                if (Children[i] is Conditional)
                {
                    childStates[i] = NodeState.None;
                }
                else
                {
                    childStates[i] = NodeState.Running;
                }
            }
        }

        protected override NodeState OnUpdate()
        {
            isRunning = false;
            for (; currentChildIndex < Children.Count; ++currentChildIndex) 
            {
                if (Children[currentChildIndex] is Conditional) continue;

                var childStat = Children[currentChildIndex].Evaluate();

                if (childStat == NodeState.Failure)
                {
                    OnAbort();
                    return NodeState.Failure;
                }

                if (childStat == NodeState.Running) isRunning = true;

                childStates[currentChildIndex] = childStat;
            }

            return isRunning == true ? NodeState.Running : NodeState.Success;
        }

        public override void OnAbort()
        {
            OnEnd();
            for(int i = 0; i < childStates.Count; ++i)
            {
                if (childStates[i] == NodeState.Running)
                {
                    Children[i].OnAbort();
                }
            }
        }
    }
}
