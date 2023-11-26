using RPG.Combat.AI.BehaviourTree.Vairable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [Description("플레이어가 보스 공격범위 내에 있으면 자식노드 실행")]
    public class IsPlayerInAttackRange : Decorator
    {
        float attackRagne;
        float dist;

        protected override void OnStart()
        {
            attackRagne = blackboard.GetData<SharedFloat>("BossAttackRagne");
            dist = blackboard.GetData<GameObject>("Owner").transform.position.GetDistance(blackboard.GetData<Transform>("Target").position);
            UnityEngine.Debug.Log(dist);
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.GetData<SharedFloat>("BossAttackRange") > dist)
            {
                return Child.Evaluate();
            }
            return NodeState.Failure;
        }
    }
}
