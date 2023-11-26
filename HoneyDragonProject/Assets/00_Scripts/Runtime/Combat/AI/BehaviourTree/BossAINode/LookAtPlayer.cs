using RPG.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class LookAtPlayer : Action
    {
        private BossAIController aiController;
        private Transform target;
        protected override void OnStart()
        {
            aiController = aiController == null ? blackboard.GetData<BossAIController>("BossAIController") : aiController;
            target = target == null ? blackboard.GetData<Transform>("Target") : target;
        }
        protected override NodeState OnUpdate()
        {
            aiController.LookAtTarget(target);
            return NodeState.Success;
        }
    }
}
