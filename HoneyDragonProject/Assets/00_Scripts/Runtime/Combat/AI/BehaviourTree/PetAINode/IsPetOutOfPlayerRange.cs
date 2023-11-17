using RPG.Combat.AI.BehaviourTree.Node;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class IsPetOutOfPlayerRange : DecoratorNode
    {
        private Transform owner;
        private GameObject player;
        private float playerRange;

        protected override void OnAwake()
        {
        }

        protected override void OnEnd()
        {
        }

        protected override void OnStart()
        {
            owner = blackboard.GetData<GameObject>("Owner").transform;
            playerRange = blackboard.GetData<float>("PlayerRange");
            player = blackboard.GetData<GameObject>("Player");
        }

        protected override NodeState OnUpdate()
        {
            if(player.transform.position.GetDistance(owner.position) > playerRange)
            {
                return Child.Evaluate();
            }

            return NodeState.Failure;
        }
    }
}
