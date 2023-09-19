using RPG.Combat;
using RPG.Core.Data;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    [RequireComponent(typeof(CombatTarget))]
    public class Enemy : Creature
    {
        EnemyData data;
        Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        public void SetData(EnemyData data)
        {
            this.data = data;
        }

        public void SetHp(int hp)
        {
            if(health is null)
            {
                health = GetComponent<Health>();
            }

            health.SetHp(hp);
        }
    }
}
