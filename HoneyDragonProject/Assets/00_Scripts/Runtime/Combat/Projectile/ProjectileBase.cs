using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace RPG.Combat.Projectile
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected float acceleration;
        [SerializeField] protected float deceleration;

        protected DamageInfo dmgInfo;
        protected Vector3 startPos;
        protected Vector3 direction;
        protected Vector3 calculatedPosition;

        protected Rigidbody rig;
        protected Transform target;
        protected ObjectPool<ProjectileBase> pool;

        protected ParticleSystem muzzleVfx;
        protected ParticleSystem hitVfx;


        [SerializeField] protected UnityEngine.GameObject muzzlePrefab;
        [SerializeField] protected UnityEngine.GameObject hitPrefab;
        [SerializeField] protected List<UnityEngine.GameObject> trails;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            muzzleVfx = Instantiate(muzzlePrefab).GetComponent<ParticleSystem>();
            hitVfx = Instantiate(hitPrefab).GetComponent<ParticleSystem>();
            rig.isKinematic = true;
            rig.useGravity = false;
        }

        public void SetPool(ObjectPool<ProjectileBase> pool)
        {
            this.pool = pool;
        }

        private void Update()
        {
            CalculatePosition();
        }

        void FixedUpdate()
        {
            ApplyPosition();
        }

        public void Fire(DamageInfo dmgInfo, Vector3 startPos, Vector3 dir, float speed, Transform target = null) 
        {
            this.transform.forward = direction = dir;
            this.transform.position = startPos;
            this.startPos = startPos;
            this.speed = speed;
            this.dmgInfo = dmgInfo;
            this.target = target;

            if (muzzleVfx is not null)
            {
                muzzleVfx .transform.position = startPos;
                muzzleVfx.transform.forward = gameObject.transform.forward;
                muzzleVfx.gameObject.SetActive(true);

                var ps = muzzleVfx.GetComponentInChildren<ParticleSystem>();
                if(ps is not null)
                {
                    StartCoroutine(DelayDisable(ps.gameObject, ps.main.duration));
                }
            }
        }

        protected IEnumerator DelayDisable(UnityEngine.GameObject obj, float time)
        {
            yield return new WaitForSeconds(time);
            obj.SetActive(false);
        }

        protected IEnumerator DestroyParticle(float waitTime)
        {
            if(transform.childCount > 0 && waitTime != 0)
            {
                List<Transform> tList = new List<Transform>();
                foreach(Transform t in transform.GetChild(0).transform)
                {
                    tList.Add(t);
                }

                var wfs = new WaitForSeconds(0.01f);

                while (transform.GetChild(0).localScale.x > 0)
                {
                    yield return wfs;
                    transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    for(int i = 0; i < tList.Count; ++i)
                    {
                        tList[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    }
                }
            }

            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }

        protected abstract void CalculatePosition();
        protected abstract void ApplyPosition();

        protected abstract void OnCollisionEnter(Collision collision);
    }
}