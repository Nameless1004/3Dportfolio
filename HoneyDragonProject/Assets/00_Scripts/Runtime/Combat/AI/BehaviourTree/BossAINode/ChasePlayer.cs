using RPG.Control;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class ChasePlayer : Action
    {
        Animator animator;
        BossAIController aiController;
        protected override void OnStart()
        {
            animator = animator == null ?blackboard.GetData<Animator>("Animator") : animator;
            aiController = aiController == null ? blackboard.GetData<BossAIController>("BossAIController") : aiController;
            animator.SetTrigger("Chase");
        }
        protected override NodeState OnUpdate()
        {
            aiController.Move();
            return NodeState.Running;
        }

        protected override void OnEnd()
        {
            animator.ResetTrigger("Chase");
        }
    }
}
