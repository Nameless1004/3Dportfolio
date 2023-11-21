using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class IsKeyDown : ConditionalNode
    {
        public KeyCode Inputkey;
        public override bool CheckCondition()
        {
            return Input.GetKeyDown(Inputkey);
        }
    }
}
