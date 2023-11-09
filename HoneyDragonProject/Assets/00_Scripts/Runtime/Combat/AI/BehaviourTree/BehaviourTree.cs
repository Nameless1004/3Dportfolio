using RPG.AI.BehaviourTree;
using RPG.Combat.AI.BehaviourTree.Node;
using RPG.Combat.AI.BehaviourTree.Node.TestNode;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree
{
    public class BehaviourTree : MonoBehaviour
    {
        NodeBase root;
        Blackboard blackboard = new Blackboard();

        private void Awake()
        {
            root = new RootNode(new SequenceNode(
                new List<NodeBase>
                {
                    //new RepeatUntilSuccess(new IsButtonClickNode()),
                    new IsInRange(new PrintTargetName())
                }, new FindTarget()));
        }

        private void Update()
        {
            root.Evaluate(blackboard);
        }
    }
}
