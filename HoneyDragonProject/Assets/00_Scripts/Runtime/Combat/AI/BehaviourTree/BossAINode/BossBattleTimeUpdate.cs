using RPG.Combat.AI.BehaviourTree.Vairable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class BossBattleTimeUpdate : Action
    {
        protected override NodeState OnUpdate()
        {
            SharedFloat elapsedTime = blackboard.GetData<SharedFloat>("BossBattleElapsedTime");
            elapsedTime.Value += Time.deltaTime;
            blackboard.SetData("BossBattleElapsedTime", elapsedTime);
            UnityEngine.Debug.Log(elapsedTime.Value);
            return NodeState.Success;
        }
    }
}
