using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace RPG.Combat.Projectile
{
    // TODO Projectile ¼öÁ¤
    public abstract class ProjectileBase : MonoBehaviour
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
        protected Transform target;
        protected ObjectPool<ProjectileBase> pool;


        [SerializeField] protected GameObject mainVfx;
        [SerializeField] protected GameObject muzzleVfx;
        [SerializeField] protected GameObject hitVfx;
        [SerializeField] protected List<UnityEngine.GameObject> trails;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            rig.useGravity = false;
        }

        public void SetPool(ObjectPool<ProjectileBase> pool)
        {
            this.pool = pool;
        }

        private void Update()
        {
            if (!IsAlive) return;
            CalculatePosition();
        }

        void FixedUpdate()
        {
            if (!IsAlive) return;
            ApplyPosition();
        }

        public void Fire(DamageInfo dmgInfo, Vector3 startPos, Vector3 dir, float speed, Transform target = null) 
        {

            IsAlive = true;

            mainVfx.gameObject.SetActive(true);
            muzzleVfx.gameObject.SetActive(true);
            hitVfx.gameObject.SetActive(true);
            rig.isKinematic = false;
            this.transform.forward = direction = dir;
            this.transform.position = startPos;
            this.startPos = startPos;
            this.speed = speed;
            this.dmgInfo = dmgInfo;
            this.target = target;

            muzzleVfx.transform.position = startPos;
            hitVfx.transform.position = startPos;

            if (muzzleVfx is not null)
            {
                muzzleVfx.gameObject.SetActive(true);
                muzzleVfx.transform.forward = gameObject.transform.forward;
                ParticleSystem ps = muzzleVfx.GetComponentInChildren<ParticleSystem>();
                if(ps is not null)
                    StartCoroutine(DelayDisable(ps.gameObject, ps.main.duration));
            }
        }

        protected IEnumerator DelayDisable(UnityEngine.GameObject obj, float time)
        {
            yield return new WaitForSeconds(time);
            obj.SetActive(false);
        }

        protected IEnumerator DestroyParticle(float waitTime)
        {
            Debug.Log("DestroyParticle");

            mainVfx.gameObject.SetActive(false);
            yield return new WaitForSeconds(waitTime);
            muzzleVfx.gameObject.SetActive(false);
            hitVfx.gameObject.SetActive(false);
            pool.Release(this);
        }

        protected abstract void CalculatePosition();
        protected abstract void ApplyPosition();

        protected abstract void OnTriggerEnter(Collider other);
    }
}