using RPG.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class StoneSlashObject : SpawnObject
    {
        [SerializeField] private float range;

        public override void Spawn(Vector3 position, Quaternion rotation, SkillData data)
        {
            transform.position = position;
            this.lifeTime = data.SpawnLifeTimeMilliSecond * 0.001f;
            minDamage = data.MinDamage;
            maxDamage = data.MaxDamage;
            transform.rotation = rotation;
            Attack();
        }

        
        private void Attack()
        {
            var colliders = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));
            foreach(var collider in colliders)
            {
                if (transform.IsBehind(collider.transform.position, 240)) continue;
                var takedamageable = collider.GetComponent<ITakeDamageable>();
                var direction = (collider.transform.position - transform.position).normalized;
                direction.y = collider.transform.position.y;
                takedamageable?.TakeDamage(new DamageInfo()
                {
                    Damage = Random.Range(minDamage, maxDamage + 1),
                    IsKnockback = true,
                    knockbackInfo = new KnockbackInfo(direction, 5f)
                });
            }
        }
    }
}