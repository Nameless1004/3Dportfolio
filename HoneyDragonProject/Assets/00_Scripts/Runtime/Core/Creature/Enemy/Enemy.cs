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
    [RequireComponent(typeof(CombatTarget))]
    public class Enemy : Creature, IPoolable<Enemy>
    {
        public ObjectPool<Enemy> Owner { get; private set; }
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
            Managers.Instance.Loot.Spawn(LootSpawManager.LootType.Exp, Controller.transform.position);
            DelayDestroy(2000).Forget();
        }

        public async UniTaskVoid DelayDestroy(int delayTime)
        {
            await UniTask.Delay(delayTime, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            Owner.Release(this);
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

        public void SetPool(ObjectPool<Enemy> owner)
        {
            this.Owner = owner;
        }

        public void OnGetAction()
        {
            gameObject.SetActive(true);
            Controller.Init();
            Controller.SetTarget(Brain.Target);
        }

        public void OnReleaseAction()
        {
            gameObject.SetActive(false);
        }

        public void OnDestroyAction()
        {
            Destroy(gameObject);
        }
    }
}
