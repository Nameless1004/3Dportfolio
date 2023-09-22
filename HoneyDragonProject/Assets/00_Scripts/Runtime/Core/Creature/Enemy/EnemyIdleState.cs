using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.State
{
    public class EnemyIdleState : StateMachineBehaviour
    {
        EnemyAIController controller;
        Enemy owner;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            owner = animator.GetComponent<Enemy>();
            controller = owner.Controller;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (controller.Target is null) return;

            if (GetDistanceToTarget(controller.transform.position, controller.Target.position) > 1)
            {
                animator.ResetTrigger("Chase");
                animator.SetTrigger("Chase");
                return;
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        private float GetDistanceToTarget(Vector3 start, Vector3 dest)
        {
            return Vector3.Distance(start, dest);
        }
    }

}

