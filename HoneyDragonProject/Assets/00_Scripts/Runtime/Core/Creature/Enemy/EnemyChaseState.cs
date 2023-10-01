using RPG.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Core.State
{
    public class EnemyChaseState : StateMachineBehaviour
    {   
        EnemyAIController controller;
        Enemy owner;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            owner = animator.GetComponent<Enemy>();
            controller = owner.Controller;
            controller.MoveTo(controller.Target.position);
            controller.Agent.speed = 1;
            controller.Agent.angularSpeed = 10000;
            controller.Agent.acceleration = 100;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            controller.StopAgent();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(controller.Target is null)
            {
                animator.ResetTrigger("Idle");
                animator.SetTrigger("Idle");
                return;
            }

            // 타겟이 공격 범위 이내이면 공격
            if (owner.Data.AttackRange >= GetDistanceToTarget(owner.transform.position, controller.Target.position))
            {
                Debug.Log("dd");
                Vector3 targetPos = controller.Target.position;
                targetPos.y = owner.transform.position.y;
                owner.transform.LookAt(targetPos);
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Attack");
                controller.Agent.isStopped = true;
                return;
            }

            // 타겟 위치로
            controller.MoveTo(controller.Target.position);
        }


        private float GetDistanceToTarget(Vector3 start, Vector3 dest)
        {
            return Vector3.Distance(start, dest);
        }
    }
}
