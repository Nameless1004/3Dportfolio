using RPG.AI.BehaviourTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class WaitNode : ActionNode
    {
        public WaitNode(float waitTime)
        {
            this.waitTime = waitTime;
        }

        float waitTime;
        float elapsedTime = 0f;

        protected override NodeState OnUpdate(Blackboard blackboard)
        {
            if (elapsedTime < waitTime)
            {
                elapsedTime += Time.deltaTime;
                return NodeState.Running;
            }
            else
            {
                return NodeState.Success;
            }
        }

        protected override void OnStart(Blackboard blackboard)
        {
            elapsedTime = 0f;
        }

        protected override void OnEnd(Blackboard blackboard)
        {
        }
    }
}
