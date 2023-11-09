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

        public override NodeState Evaluate(Blackboard blackboard)
        {
            if (elapsedTime < waitTime)
            {
                elapsedTime += Time.deltaTime;
                return NodeState.Running;
            }
            else
            {
                elapsedTime = 0f;
                return NodeState.Success;
            }
        }
    }
}
