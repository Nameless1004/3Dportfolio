using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : StateMachineBehaviour
{
    Boss boss;
    float moveSpeed;
    Rigidbody rigidBody;
    GridController gridController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
        moveSpeed = boss.Data.MoveSpeed;
        rigidBody = boss.GetComponent<Rigidbody>();
        gridController = boss.Grid;
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
        rigidBody.position += direction * moveSpeed * Time.deltaTime;
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
