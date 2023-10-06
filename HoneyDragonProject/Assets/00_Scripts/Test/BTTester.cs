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
        RootNode node = new RootNode(new IfNode(
            () => Input.GetMouseButtonDown(0),
            new SequenceNode(new List<Node>
            {
                new RepeatNode(5, new ActionNode(() => { Debug.Log("...1"); })),
                new RepeatNode(5, new ActionNode(() => { Debug.Log("...2"); })),
                new RepeatNode(5, new ActionNode(() => { Debug.Log("...3"); })),
            })
            ));
        return node;
    }

    private void Update()
    {
        bt.UpdateTree();
    }
}
