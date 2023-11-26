using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public abstract class Conditional : NodeBase
    {
        protected override NodeState OnUpdate()
        {
            return CheckCondition() == true ? NodeState.Success : NodeState.Failure;
        }
        public abstract bool CheckCondition();
    }
}
