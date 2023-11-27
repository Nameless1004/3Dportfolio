using RPG.Combat.AI.BehaviourTree.Variable;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class PlayerInAttackRange : Conditional
    {
        float attackRagne;
        float dist;
        protected override void OnStart()
        {
            attackRagne = blackboard.GetData<SharedVariable<float>>("BossAttackRange");
            dist = blackboard.GetData<GameObject>("Owner").transform.position.GetDistance(blackboard.GetData<Transform>("Target").position);
        }

        public override bool CheckCondition()
        {
            if (attackRagne > dist)
            {
                return true;
            }
            return false;
        }
    }
}
