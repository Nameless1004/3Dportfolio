using RPG.Core;
using UnityEngine;
using UnityEngine.Pool;

namespace RPG.Combat.Skill
{
    public class SpawnObject : MonoBehaviour, IPoolable<SpawnObject>
    {
        ObjectPool<SpawnObject> owner;
        Collider collider;
        private bool isSpawned = false;
        private float lifeTime = 0f;
        private float elapsedTime = 0f;


        private void Awake()
        {
            collider = GetComponent<Collider>();
        }

        public void OnDestroyAction()
        {
            gameObject.SetActive(false);
            isSpawned = false;
        }

        public void OnGetAction()
        {
            gameObject.SetActive(true);
            elapsedTime = 0f;
            isSpawned = true;
        }

        public void OnReleaseAction()
        {
            gameObject.SetActive(false);
            isSpawned = false;
        }

        public void SetPool(ObjectPool<SpawnObject> owner)
        {
            this.owner = owner;
        }

        private void Update()
        {
            if (isSpawned == false) return;

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= lifeTime)
            {
                owner.Release(this);
                return;
            }
        }

        public void Spawn(Vector3 position, float lifeTime)
        {
            transform.position = position;
            this.lifeTime = lifeTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.IsSameLayer("Enemy"))
            {
                var takedamageable = other.GetComponent<ITakeDamageable>();
                takedamageable?.TakeDamage(new DamageInfo()
                {
                    Damage = Random.Range(3, 5),
                    IsKnockback = true,
                    knockbackInfo = new KnockbackInfo() { force = 10f, 
                        type = KnockbackInfo.KnockbackType.Explosion, 
                        point = transform.position, radius = 3f }
                });
            }
        }


    }
}
