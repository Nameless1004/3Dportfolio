using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class IsMouseButtonDownNode : ConditionalNode
    {
        [Range(0, 1)]
        public int button;

        public override bool CheckCondition()
        {
            return Input.GetMouseButtonDown(button);
        }
    }
}
