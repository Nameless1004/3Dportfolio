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

        protected override void Awake()
        {
            base.Awake();
            Target = Managers.Instance.Game.CurrentPlayer;
            // temp
            Data = Managers.Instance.Data.BossDataDict[10000];
            SetHp(Data.Hp);
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
            Destroy(gameObject);
        }

        public bool IsTargetInAttackRange()
        {
            return Vector3.Distance(transform.position, Target.position) < Data.AttackRange;
        }
    }
}