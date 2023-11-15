using RPG.AI.BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Composite/RandomSelector")]
    public class RandomSelectors : CompositeNode
    {
        protected override void OnEnd()
        {
        }

        protected override void OnStart()
        {
        }

        // 자식노드 중 하나라도 success, running을 반환하면 부모에게 반환
        protected override NodeState OnUpdate()
        {
            if (Children.Count == 0) return NodeState.Failure;
            

            for(int i = 0; i < Children.Count; i++)
            {
                int curIndex = Random.Range(0, Children.Count);
                var childState = Children[curIndex].Evaluate();
                if(childState == NodeState.Success || childState == NodeState.Running) 
                {
                    return childState;
                }
            }

            return NodeState.Failure;
        }
    }
}
