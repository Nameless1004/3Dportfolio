using Cysharp.Threading.Tasks;
using RPG.Combat;
using RPG.Control;
using RPG.Core.Data;
using RPG.Core.Manager;
using RPG.Util;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace RPG.Core
{
    public abstract class Enemy : Creature
    {
        public EnemyData Data { get; protected set; }
        public Health Health { get; protected set; }

        protected virtual void Awake()
        {
            Health = GetComponent<Health>();
        }

        protected abstract void OnDie();

        public void SetData(EnemyData data)
        {
            this.Data = data;
            Health.SetHp(data.Hp);
        }

        public void SetHp(int hp)
        {
            if(Health is null)
            {
                Health = GetComponent<Health>();
            }

            Health.SetHp(hp);
        }
    }
}
