using Cysharp.Threading.Tasks;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace RPG.Combat.Projectile
{
    public abstract class ProjectileBase : MonoBehaviour, IPoolable<ProjectileBase>
    {
        [SerializeField] protected float speed;
        [SerializeField] protected float acceleration;
        [SerializeField] protected float deceleration;

        protected DamageInfo dmgInfo;
        protected Vector3 startPos;
        protected Vector3 direction;
        protected Vector3 calculatedPosition;
        protected bool IsAlive;

        protected Rigidbody rig;
        protected Creature target;
        protected ObjectPooler<ProjectileBase> pool;


        [SerializeField] protected GameObject mainVfx;
        [SerializeField] protected GameObject muzzleVfx;
        [SerializeField] protected GameObject hitVfx;
        [SerializeField] protected List<UnityEngine.GameObject> trails;

        protected bool isDestroyed = false;
        public bool IsDestroyed => isDestroyed;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            rig.useGravity = false;
        }

        public void SetPool(ObjectPooler<ProjectileBase> pool)
        {
            this.pool = pool;
        }

        public ProjectileBase GetPooledObject() => this;

        //private void Update()
        //{
        //    if (!IsAlive) return;
        //    CalculatePosition();
        //}

        void FixedUpdate()
        {
            if (!IsAlive) return;
            CalculatePosition();
            ApplyPosition();
        }

        public void Fire(DamageInfo dmgInfo, Vector3 startPos, Vector3 dir, float speed, Creature target = null) 
        {
            this.transform.forward = direction = dir;
            this.transform.position = startPos;
            this.startPos = startPos;
            this.speed = speed;
            this.dmgInfo = dmgInfo;
            this.target = target;

            Reset();

            muzzleVfx.gameObject.SetActive(true);
            muzzleVfx.transform.forward = gameObject.transform.forward;
            ParticleSystem ps = muzzleVfx.GetComponentInChildren<ParticleSystem>(true);
            DelayDisable(muzzleVfx, (int)(ps.main.duration * 1000f)).Forget();
        }

        protected virtual void Reset()
        {
            IsAlive = true;

            mainVfx.SetActive(true);
            muzzleVfx.SetActive(true);
            hitVfx.SetActive(false);
        }

        protected async UniTaskVoid DelayDisable(UnityEngine.GameObject obj, int milliTime)
        {
            await UniTask.Delay(milliTime, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            obj.SetActive(false);
        }

        protected async UniTaskVoid DestroyParticle(int milliTime)
        {
            Debug.Log("DestroyParticle");

            mainVfx.SetActive(false);
            await UniTask.Delay(milliTime, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());

            muzzleVfx.SetActive(false);
            hitVfx.SetActive(false);
            pool.Release(this);
        }

        protected abstract void CalculatePosition();
        protected abstract void ApplyPosition();

        protected abstract void OnTriggerEnter(Collider other);

        private void OnDestroy()
        {
            isDestroyed = true;
        }

        public void OnGetAction()
        {
            if (IsDestroyed == true) return;

            gameObject.SetActive(true);
        }

        public void OnReleaseAction()
        {
            gameObject.SetActive(false);
        }

        public void OnDestroyAction()
        {
            gameObject.SetActive(false);
        }
    }
}