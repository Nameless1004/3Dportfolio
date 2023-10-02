using RPG.Control;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RPG.Core.State
{
    public class EnemyAttackState : StateMachineBehaviour
    {
        EnemyAIController controller;
        Enemy owner;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            owner = animator.GetComponent<Enemy>();
            controller = owner.Controller;
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

            if(owner.Data.AttackRange < GetDistanceToTarget(controller.Target.position, controller.transform.position))
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
