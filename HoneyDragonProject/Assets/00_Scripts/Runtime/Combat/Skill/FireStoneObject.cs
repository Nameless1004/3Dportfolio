using RPG.Core.Data;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class FireStoneObject : SpawnObject
    {
        private TrailRenderer trail;
        private Vector3 startPosition;
        public override void Spawn(Vector3 position, Quaternion rotation, SkillData data)
        {
            if (trail == null)
            {
                trail = GetComponentInChildren<TrailRenderer>();
            }
            trail.enabled = false;

            transform.position = position;
            startPosition = position;
            this.lifeTime = data.SpawnLifeTimeMilliSecond * 0.001f;
            minDamage = data.MinDamage;
            maxDamage = data.MaxDamage;
            transform.rotation = rotation;
            speed = data.Speed;

            trail.Clear();
            trail.enabled = true;
        }

        protected override void Update()
        {
            base.Update();
            transform.position = Util.MathGraph.GetSpiralPosition(startPosition, elapsedTime,speed, 6f);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.IsSameLayer("Enemy"))
            {
                var takedamageable = other.GetComponent<ITakeDamageable>();
                takedamageable?.TakeDamage(new DamageInfo()
                {
                    Damage = Random.Range(minDamage, maxDamage + 1),
                    IsKnockback = true,
                    knockbackInfo = new KnockbackInfo()
                    {
                        force = 10f,
                        type = KnockbackInfo.KnockbackType.Explosion,
                        point = transform.position,
                        radius = 3f
                    }
                });
            }
        }
    }
}
