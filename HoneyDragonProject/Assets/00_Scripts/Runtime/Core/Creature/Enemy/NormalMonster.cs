using Cysharp.Threading.Tasks;
using RPG.Control;
using RPG.Core.Manager;
using UnityEngine.Pool;

namespace RPG.Core
{
    public class NormalMonster : Enemy, IPoolable<NormalMonster>
    {
        public ObjectPool<NormalMonster> Owner { get; private set; }
        public NormalMonsterAIController Controller { get; private set; }
        public bool IsReleased = false;

        private void OnEnable()
        {
            Health.OnDie -= OnDie;
            Health.OnDie += OnDie;
            Health.OnHit -= OnHit;
            Health.OnHit += OnHit;
        }

        private void OnDisable()
        {
            Health.OnDie -= OnDie;
            Health.OnHit -= OnHit;
        }

        public void SetPool(ObjectPool<NormalMonster> owner)
        {
            Owner = owner;
        }

        protected override void OnDie()
        {
            Managers.Instance.Loot.Spawn(LootSpawManager.LootType.Exp, Controller.transform.position);
            DelayDestroy(2000).Forget();
        }

        protected override void Awake()
        {
            base.Awake();
            Controller = GetComponent<NormalMonsterAIController>();
        }

        public async UniTaskVoid DelayDestroy(int delayTime)
        {
            await UniTask.Delay(delayTime, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            Owner.Release(this);
        }

        public void OnGetAction()
        {
            gameObject.SetActive(true);
            Controller.Init();
            IsReleased = false;
        }

        public void OnReleaseAction()
        {
            gameObject.SetActive(false);
            IsReleased = true;
        }

        public void OnDestroyAction()
        {
            Destroy(gameObject);
        }
    }
}
