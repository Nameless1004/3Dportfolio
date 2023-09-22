using RPG.Combat;
using RPG.Control;
using RPG.Core.Data;
using RPG.Core.Manager;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    [RequireComponent(typeof(CombatTarget))]
    public class Enemy : Creature
    {
        public EnemyData Data { get; private set; }
        public EnemyBrain Brain { get; private set; }
        public EnemyAIController Controller { get; private set; }
        public Health Health { get; private set; }

        private void Awake()
        {
            Health = GetComponent<Health>();
            Brain = GetComponent<EnemyBrain>();
            Controller = GetComponent<EnemyAIController>();
        }

        private void Start()
        {
            Controller.SetTarget(Brain.Target);
        }

        private void OnEnable()
        {
            Health.OnDie -= OnDie;
            Health.OnDie += OnDie;
        }

        private void OnDisable()
        {
            Health.OnDie -= OnDie;
        }

        public void OnDie()
        {
            Controller.Die();
        }

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
