using RPG.Combat.AI.BehaviourTree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.BossAINode
{
    public class BossDie : Conditional
    {
        Health bossHealth;
        protected override void OnStart()
        {
            bossHealth = bossHealth == null ? blackboard.GetData<Health>("Health") : bossHealth;
        }
        public override bool CheckCondition()
        {
            return bossHealth.IsAlive == false;
        }
    }
}
