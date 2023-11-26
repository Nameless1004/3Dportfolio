using RPG.Combat.AI.BehaviourTree.Node;
using RPG.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.BossAINode
{
    public class IsBossAttackEnd : Decorator
    {
        private BossAIController controller;
        
        protected override void OnStart()
        {
            controller = controller == null ? blackboard.GetData<BossAIController>("BossAIController") : controller;
        }

        protected override NodeState OnUpdate()
        {
            if(controller.IsAttackEnd == true)
            {
                return Child.Evaluate();
            }

            return NodeState.Failure;
        }
    }
}
