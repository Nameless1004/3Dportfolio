using UnityEngine;

namespace RPG.Combat.Skill
{
    public class LightningBoltObject : SpawnObject
    {

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
