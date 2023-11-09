using RPG.AI.BehaviourTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class PrintTargetName : ActionNode
    {
        public override NodeState Evaluate(Blackboard blackboard)
        {
            var target = (GameObject)blackboard.GetData("Target");
            Debug.Log(target?.name ?? "없음");
            return NodeState.Success;
        }
    }
}
