using Cysharp.Threading.Tasks;
using RPG.Combat;
using RPG.Control;
using RPG.Core;
using RPG.Core.Data;
using RPG.Core.Manager;
using UnityEngine;

namespace RPG.Core
{
    public class Boss : Enemy
    {
        public Creature Target;
        public GridController Grid;

        protected override void Awake()
        {
            base.Awake();
            Target = Managers.Instance.Game.CurrentPlayer;
            Grid = Managers.Instance.Stage.CurrentStage.Grid;

            // temp
            Data = Managers.Instance.Data.BossDataDict[10000];
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

        protected override void OnDie()
        {
            // Managers.Instance.Loot.Spawn(LootSpawManager.LootType.Exp, transform.position);
        }

        public void SetData(EnemyData data)
        {
            this.Data = data;
            Health.SetHp(data.Hp);
        }

        public void SetHp(int hp)
        {
            if (Health is null)
            {
                Health = GetComponent<Health>();
            }

            Health.SetHp(hp);
        }

        public bool IsTargetInAttackRange()
        {
            return Vector3.Distance(transform.position, Target.position) < Data.AttackRange;
        }
    }
}