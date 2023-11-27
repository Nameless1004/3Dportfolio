using RPG.Combat.AI.BehaviourTree.Variable;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [Description("플레이어가 공격 범위 밖으로 나가면 부모노드 중단")]
    public class PlayerOutOfAttackRange : Conditional
    {
        float attackRagne;
        float dist;
        protected override void OnStart()
        {
            attackRagne = blackboard.GetData<SharedVariable<float>>("BossAttackRange");
            dist = blackboard.GetData<GameObject>("Owner").transform.position.GetDistance(blackboard.GetData<Transform>("Target").position);
            UnityEngine.Debug.Log(dist);
        }

        public override bool CheckCondition()
        {
            if (attackRagne < dist)
            {
                return true;
            }
            return false;
        }
    }
}
