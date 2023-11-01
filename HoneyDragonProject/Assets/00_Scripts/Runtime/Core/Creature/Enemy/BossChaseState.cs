using RPG.Core;
using RPG.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : StateMachineBehaviour
{
    Boss boss;
    float moveSpeed;
    Rigidbody rigidBody;
    GridController gridController;

    float currentVelocity;
    float smoothTime = 0.2f;

    private void Rotate(Vector3 direction)
    {
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(boss.transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        boss.transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
        moveSpeed = boss.Data.MoveSpeed;
        rigidBody = boss.GetComponent<Rigidbody>();
        gridController = Managers.Instance.Stage.CurrentStage.Grid;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boss.IsTargetInAttackRange() == true)
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Attack");
            return;
        }

        Cell cellBlow = gridController.CurFlowField.GetCellFromWorldPos(boss.position);
        Vector3 direction = new Vector3(cellBlow.BestDirection.Vector.x, 0, cellBlow.BestDirection.Vector.y);
        if (direction == Vector3.zero)
        {
            direction = (boss.Target.position - boss.position).normalized;
        }
        rigidBody.rotation = Quaternion.LookRotation(direction);
        rigidBody.position += direction * moveSpeed * Time.deltaTime;
        Rotate(direction);
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
