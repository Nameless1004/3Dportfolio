using RPG.Combat.AI.BehaviourTree.Vairable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class BossBattleTimeout : Conditional
    {
        public override bool CheckCondition()
        {
            if (blackboard.GetData<SharedFloat>("BossBattleElapsedTime") > blackboard.GetData<SharedFloat>("BossBattleLimitTime"))
            {
                return true;
            }

            return false;
        }
    }
}
