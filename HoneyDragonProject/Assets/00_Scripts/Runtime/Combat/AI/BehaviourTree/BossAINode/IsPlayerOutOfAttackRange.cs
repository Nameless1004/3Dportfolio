using RPG.Combat.AI.BehaviourTree.Variable;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [Description("플레이어가 보스공격범위 밖으로 나가면 자식노드 실행")]
    public class IsPlayerOutOfAttackRange : Decorator
    {
        float attackRagne;
        float dist;

        protected override void OnStart()
        {
            attackRagne = blackboard.GetData<SharedVariable<float>>("BossAttackRange");
            dist = blackboard.GetData<GameObject>("Owner").transform.position.GetDistance(blackboard.GetData<Transform>("Target").position);
        }

        protected override NodeState OnUpdate()
        {
            if(attackRagne < dist)
            {
                return Child.Evaluate();
            }
            return NodeState.Failure;
        }
    }
}
