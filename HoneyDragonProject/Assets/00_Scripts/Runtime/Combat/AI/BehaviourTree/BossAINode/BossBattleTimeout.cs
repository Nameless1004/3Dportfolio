using RPG.Combat.AI.BehaviourTree.Variable;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class BossBattleTimeout : Conditional
    {
        public override bool CheckCondition()
        {
            if (blackboard.GetData<SharedVariable<float>>("BossBattleElapsedTime") > blackboard.GetData<SharedVariable<float>>("BossBattleLimitTime"))
            {
                return true;
            }

            return false;
        }
    }
}
