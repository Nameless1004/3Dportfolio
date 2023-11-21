using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.Node.Decorators
{
    public class ReturnFailNode : DecoratorNode
    {
        protected override NodeState OnUpdate()
        {
            Child.Evaluate();
            return NodeState.Failure;
        }
    }
}
