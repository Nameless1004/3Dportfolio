using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class MoveToNode : ActionNode
    {
        public Transform owner;
        public Vector2 destination;
        protected override void OnEnd()
        {
        }

        protected override void OnStart()
        {
            destination = (Vector2)blackboard.GetData("clickPosition");
            owner = (blackboard.GetData("owner") as GameObject).transform;
        }

        protected override NodeState OnUpdate()
        {
            if((Vector2)owner.position == destination)
            {
                return NodeState.Success;
            }
            else
            {
                owner.position = Vector2.MoveTowards(owner.position, destination, 2f * Time.deltaTime);
                return NodeState.Running;
            }
            
        }
    }
}
