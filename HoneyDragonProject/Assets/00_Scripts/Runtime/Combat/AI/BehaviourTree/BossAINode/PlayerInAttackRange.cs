using RPG.Combat.AI.BehaviourTree.Vairable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class PlayerInAttackRange : Conditional
    {
        float attackRagne;
        float dist;
        protected override void OnStart()
        {
            attackRagne = blackboard.GetData<SharedFloat>("BossAttackRange");
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
