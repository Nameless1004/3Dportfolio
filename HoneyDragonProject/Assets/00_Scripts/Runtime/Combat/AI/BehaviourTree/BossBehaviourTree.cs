using RPG.Combat.AI.BehaviourTree.Vairable;
using RPG.Control;
using RPG.Core;
using RPG.Core.Data;
using RPG.Core.Manager;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree
{
    [CreateAssetMenu(menuName = "BehaviourTree/Tree/Boss")]
    public class BossBehaviourTree : BehaviourTree
    {
        protected override void CreateBlackboard(GameObject owner)
        {
            blackboard = new Blackboard();
            blackboard.SetData<GameObject>("Owner", owner);
            blackboard.SetData<Health>("Health", owner.GetComponentInChildren<Health>());
            blackboard.SetData<Boss>("Boss", owner.GetComponentInChildren<Boss>());
            blackboard.SetData<BossAIController>("BossAIController", owner.GetComponentInChildren<BossAIController>());
            blackboard.SetData<Animator>("Animator", owner.GetComponentInChildren<Animator>());
            blackboard.SetData<Rigidbody>("RigidBody", owner.GetComponentInChildren<Rigidbody>());
            blackboard.SetData<Transform>("Target", Managers.Instance.Game.CurrentPlayer.transform);
            blackboard.SetData<SharedFloat>("BossBattleElapsedTime", 0f);
            blackboard.SetData<SharedFloat>("BossBattleLimitTime", 500f);
            blackboard.SetData<SharedFloat>("BossAttackRange", (blackboard["Boss"] as Boss).Data.AttackRange);
        }
    }
}
