using Cysharp.Threading.Tasks;
using RPG.Combat.AI.BehaviourTree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace RPG.Combat.AI.BehaviourTree
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        public int milliseconds;
        public BehaviourTree tree;
        public NodeState TreeState = NodeState.Running;

        private GameObject owner;
        private bool isTreeRunning = false;

        public void TreeInit(GameObject ownerObject)
        {
            owner = ownerObject;
            isTreeRunning = true;
            tree = tree.Clone();
            tree.Bind(owner);
        }

        public void Run()
        {
            if(isTreeRunning == false)
            {
                TreeInit(owner);
            }

            TreeUpdate().Forget();
        }

        private async UniTaskVoid TreeUpdate()
        {
            while(true)
            {
                if(TreeState == NodeState.Running)
                {
                    TreeState = tree.TreeUpdate();
                }

                if (milliseconds > 0)
                {
                    await UniTask.Delay(milliseconds, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
                }
                else
                {
                    await UniTask.Yield(this.GetCancellationTokenOnDestroy());
                }
            }
        }
    }
}
