using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class IsMouseButtonDown : ActionNode
    {
        protected override void OnEnd()
        {
        }

        protected override void OnStart()
        {
        }

        protected override NodeState OnUpdate()
        {
            if(Input.GetMouseButtonDown(1))
            {
                blackboard.SetData("clickPosition", (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
                return NodeState.Success;
            }

            return NodeState.Failure;
        }
    }
}
