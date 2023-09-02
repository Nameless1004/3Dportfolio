using RPG.Control;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RPG.Core.Creature.State
{
    public class EnemyAttackState : StateMachineBehaviour
    {
        EnemyAIController controller;
        Enemy owner;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            controller = animator.GetComponent<EnemyAIController>();
            owner = animator.GetComponent<Enemy>();

            controller.Agent.isStopped = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(controller.Target is null)
            {
                animator.ResetTrigger("StopAttack");
                animator.SetTrigger("StopAttack");
                return;
            }

            if(GetDistanceToTarget(controller.Target.position, controller.transform.position) > owner.Data.AttackRange)
            {
                animator.ResetTrigger("Chase");
                animator.SetTrigger("Chase");
                return;
            }
        }


        private float GetDistanceToTarget(Vector3 start, Vector3 dest)
        {
            return Vector3.Distance(start, dest);
        }
    }
}
