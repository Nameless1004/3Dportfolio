using Cysharp.Threading.Tasks;
using RPG.Combat.AI.BehaviourTree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        public int milliseconds;
        public BehaviourTree tree;


        private void Start()
        {
            tree = tree.Clone();
            tree.Bind();
            tree.blackboard.SetData("owner", gameObject);

            TreeUpdate().Forget();
        }
        private 
            async UniTaskVoid TreeUpdate()
        {
            while(true)
            {
                tree.TreeUpdate();
                if(milliseconds > 0)
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
