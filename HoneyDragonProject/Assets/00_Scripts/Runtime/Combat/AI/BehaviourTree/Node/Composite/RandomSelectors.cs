using RPG.AI.BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    [CreateAssetMenu(menuName = "BehaviourTree/Node/Composite/RandomSelector")]
    public class RandomSelectors : CompositeNode
    {
        public RandomSelectors(List<NodeBase> nodes)
        {
            Children = nodes;
        }

        protected override void OnEnd(Blackboard blackboard)
        {
        }

        protected override void OnStart(Blackboard blackboard)
        {
        }

        // �ڽĳ�� �� �ϳ��� success, running�� ��ȯ�ϸ� �θ𿡰� ��ȯ
        protected override NodeState OnUpdate(Blackboard blackboard)
        {
            if (Children.Count == 0) return NodeState.Failure;
            

            for(int i = 0; i < Children.Count; i++)
            {
                int curIndex = Random.Range(0, Children.Count);
                var childState = Children[curIndex].Evaluate(blackboard);
                if(childState == NodeState.Success || childState == NodeState.Running) 
                {
                    return childState;
                }
            }

            return NodeState.Failure;
        }
    }
}
