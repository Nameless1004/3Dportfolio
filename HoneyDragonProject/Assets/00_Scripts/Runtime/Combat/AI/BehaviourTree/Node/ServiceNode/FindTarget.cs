using RPG.AI.BehaviourTree;
using RPG.Runtime.Combat.AI.BehaviourTree.Node;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Node
{
    public class FindTarget : ServiceNode
    {
        public override void Update(Blackboard blackboard)
        {
            Vector2 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var col = Physics2D.OverlapCircle(mouseVector, 10f, LayerMask.GetMask("Enemy"));
            if(col != null)
            {
                blackboard.SetData("Target", col.gameObject);
            }
            else blackboard.SetData("Target", null);
        }
    }
}
