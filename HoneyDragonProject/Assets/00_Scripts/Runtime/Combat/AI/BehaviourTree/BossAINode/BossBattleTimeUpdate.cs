using RPG.Combat.AI.BehaviourTree.Variable;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class BossBattleTimeUpdate : Action
    {
        protected override NodeState OnUpdate()
        {
            SharedVariable<float> elapsedTime = blackboard.GetData<SharedVariable<float>>("BossBattleElapsedTime");
            elapsedTime.Value += Time.deltaTime;
            return NodeState.Success;
        }
    }
}
