using RPG.AI.BehaviourTree;
using RPG.Combat.AI.BehaviourTree.Node;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree
{
    [CreateAssetMenu(menuName ="BehaviourTree/Tree")]
    public class BehaviourTree : ScriptableObject
    {
        public RootNode root;
        public GameObject owner;
        public Blackboard blackboard = new Blackboard();

        public void TreeUpdate()
        {
            root.Evaluate(blackboard);
        }

        public BehaviourTree Clone()
        {
            BehaviourTree tree = Instantiate(this);
            tree.root = (RootNode)root.Clone();
            return tree;
        }

    }
}
