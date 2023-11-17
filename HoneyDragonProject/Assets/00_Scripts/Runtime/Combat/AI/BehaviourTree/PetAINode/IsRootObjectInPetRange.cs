using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class IsRootObjectInPetRange : DecoratorNode
    {
        public Transform Owner;
        public float PetRange;

        protected override void OnAwake()
        {
        }

        protected override void OnEnd()
        {
        }

        protected override void OnStart()
        {
            Owner = (blackboard["Owner"] as GameObject).transform;
            var colliders = Physics.OverlapSphere(Owner.position, (float)blackboard["PetRange"], LayerMask.GetMask("Loot"));
            if(colliders.Length != 0)
            {
                blackboard["LootObject"] = colliders[0].gameObject;
            }
            else
            {
                blackboard["LootObject"] = null;
            }
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard["LootObject"] != null)
            {
                return Child.Evaluate();
            }

            return NodeState.Failure;
        }
    }
}
