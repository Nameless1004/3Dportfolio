using RPG.Control;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class BossAttack : Action
    {
        Animator animator;
        BossAIController controller;
        protected override void OnStart()
        {
            animator = animator == null ? blackboard.GetData<Animator>("Animator") : animator;
            controller = controller == null ? blackboard.GetData<BossAIController>("BossAIController") : controller;
            controller.IsAttackEnd = false;
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Attack");
        }

        protected override NodeState OnUpdate()
        {
            if(controller.IsAttackEnd == false)
            {
                return NodeState.Running;
            }
            
            return NodeState.Success;
        }

        public override void OnAbort()
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Idle");
            controller.IsAttackEnd = true;
            UnityEngine.Debug.Log("Aborted!");
        }
    }
}
