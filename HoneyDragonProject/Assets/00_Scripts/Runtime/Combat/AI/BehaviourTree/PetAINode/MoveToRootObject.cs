using RPG.Core;
using RPG.Core.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class MoveToRootObject : ActionNode
    {
        public Transform owner;
        public GameObject lootObject;
        public float moveSpeed;
        public float pickupRange;

        protected override void OnStart()
        {
            lootObject = blackboard["LootObject"] as GameObject;
            owner = (blackboard["Owner"] as GameObject).transform;
            moveSpeed = (float)blackboard["MoveSpeed"];
            pickupRange = (float)blackboard["PickupRange"];
        }

        protected override NodeState OnUpdate()
        {
            if (lootObject == null) return NodeState.Failure;

            owner.position = Vector3.MoveTowards(owner.position, lootObject.transform.position, moveSpeed * Time.deltaTime);

            if(owner.position.GetDistance(lootObject.transform.position) <= pickupRange)
            {
                lootObject.GetComponent<Loot>().Get(owner.GetComponent<Pet>());
                Destroy(lootObject);
                return NodeState.Success;
            }
            


            return NodeState.Running;
        }
    }
}
