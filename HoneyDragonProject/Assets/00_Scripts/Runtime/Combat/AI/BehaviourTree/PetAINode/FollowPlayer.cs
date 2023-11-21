using RPG.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class FollowPlayer : ActionNode
    {
        private GameObject player;
        private Transform owner;
        private float followSpeed;
        private float playerRange;


        protected override void OnStart()
        {
            owner = blackboard.GetData<GameObject>("Owner").transform;
            player = blackboard.GetData<GameObject>("Player");
            followSpeed = blackboard.GetData<float>("MoveSpeed");
            playerRange = blackboard.GetData<float>("PlayerRange");
        }

        protected override NodeState OnUpdate()
        {
            Vector3 dir = player.transform.position - owner.position;
            dir.y = 0f;
            dir = dir.normalized;
            owner.position += dir * followSpeed * Time.deltaTime;

            // 범위 1/2에 들어오게
            float dist = player.transform.position.GetDistance(owner.position);
            //if (player.transform.position.GetDistance(owner.position) < playerRange)
            //{
            //    return NodeState.Success;
            //}
            return NodeState.Running;

        }
    }
}
