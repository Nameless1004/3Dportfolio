using RPG.Control;
using RPG.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree
{
    [CreateAssetMenu(menuName = "BehaviourTree/Tree/Pet")]
    public class PetBehaviourTree : BehaviourTree
    {
        protected override void CreateBlackboard(GameObject owner)
        {
            blackboard = new Blackboard();
            blackboard.SetData("Owner", owner);
            blackboard.SetData("Player", blackboard.GetData<GameObject>("Owner").GetComponent<PetAIController>().Player);
            blackboard.SetData("MoveSpeed", 4f);
            blackboard.SetData("PlayerRange", 10f);
            blackboard.SetData("PetRange", 20f);
            blackboard.SetData("PickupRange", 5f);
            blackboard.SetData("LootObject", null);
        }
    }
}
