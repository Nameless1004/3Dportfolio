using Cysharp.Threading.Tasks;
using RPG.Core;
using RPG.Core.Data;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

namespace RPG.Combat.Skill
{
    public class SpawnObject : MonoBehaviour, IPoolable<SpawnObject>
    {
        protected ObjectPool<SpawnObject> owner;
        protected CancellationTokenSource cancellationTokenSource;
        protected bool isSpawned = false;
        protected float lifeTime = 0f;
        protected float elapsedTime = 0f;
        protected int minDamage;
        protected int maxDamage;
        protected float speed;


        public void OnDestroyAction()
        {
            gameObject.SetActive(false);
            isSpawned = false;
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

        public void OnGetAction()
        {
            gameObject.SetActive(true);
            elapsedTime = 0f;
            isSpawned = true;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public void OnReleaseAction()
        {
            gameObject.SetActive(false);
            isSpawned = false;
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
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

        public virtual void Spawn(Vector3 position, SkillData data)
        {
            transform.position = position;
            this.lifeTime = data.SpawnLifeTimeMilliSecond * 0.001f;
            minDamage = data.MinDamage;
            maxDamage = data.MaxDamage;
            speed = data.Speed;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
        }


    }
}
