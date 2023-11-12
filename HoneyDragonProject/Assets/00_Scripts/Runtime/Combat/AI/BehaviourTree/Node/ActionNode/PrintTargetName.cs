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
        protected override void OnEnd(Blackboard blackboard)
        {
            throw new NotImplementedException();
        }

        protected override void OnStart(Blackboard blackboard)
        {
            throw new NotImplementedException();
        }

        protected override NodeState OnUpdate(Blackboard blackboard)
        {
            var target = (GameObject)blackboard.GetData("Target");
            Debug.Log(target?.name ?? "없음");
            return NodeState.Success;
        }
    }
}
