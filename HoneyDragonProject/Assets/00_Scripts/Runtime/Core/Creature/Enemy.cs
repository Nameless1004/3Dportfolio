using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core.Creature
{
    public class Enemy : Creature
    {
        private EnemyData data;
        public EnemyData Data => data;

        private void Awake()
        {
            data = new EnemyData() { MoveSpeed = 1, AttackRange = 3 };
        }
    }
}
