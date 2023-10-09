using System;
using System.Collections.Generic;
using UnityEngine;

public  class BTTester: MonoBehaviour
{
    BehaviourTree bt;
    private void Awake()
    {
        var root = CreateNode();

        bt = new BehaviourTree(root);
    }

    private RootNode CreateNode()
    {
        var moveAction = new MoveAction();
        var condition = new IfMousePositionInRange();
        condition.pivot = transform.position;
        moveAction.Target = transform;
        condition.Child = moveAction;
        RootNode node = new RootNode(new SequenceNode(new List<Node>
        {
            condition
        }));

        return node;
    }

    private void Update()
    {
        bt.UpdateTree();
    }
}
