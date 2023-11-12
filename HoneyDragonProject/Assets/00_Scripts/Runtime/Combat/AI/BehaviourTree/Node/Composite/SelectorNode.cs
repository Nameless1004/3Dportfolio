using RPG.AI.BehaviourTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Composite/Selector")]
    public class SelectorNode : CompositeNode
    {

        protected override void OnEnd(Blackboard blackboard)
        {
        }

        protected override void OnStart(Blackboard blackboard)
        {
        }

        protected override NodeState OnUpdate(Blackboard blackboard)
        {
            for(int i= 0; i < Children.Count; ++i)
            {
                var childState = Children[i].Evaluate(blackboard);
                if (childState == NodeState.Success || childState == NodeState.Running)
                    return childState;
            }

            return NodeState.Failure; 
        }
    }
}
