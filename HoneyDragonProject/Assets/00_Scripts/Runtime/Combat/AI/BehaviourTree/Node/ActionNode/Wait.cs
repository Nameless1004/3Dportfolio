using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class Wait : Action
    {
        public float waitTime;
        float elapsedTime = 0f;

        protected override NodeState OnUpdate()
        {
            if (elapsedTime < waitTime)
            {
                elapsedTime += Time.deltaTime;
                return NodeState.Running;
            }
            else
            {
                return NodeState.Success;
            }
        }


        protected override void OnStart()
        {
            elapsedTime = 0f;
        }

        protected override void OnEnd()
        {
            elapsedTime = 0f;
        }

    }
}
