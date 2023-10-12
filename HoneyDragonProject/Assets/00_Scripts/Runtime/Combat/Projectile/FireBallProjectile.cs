using UnityEngine;

namespace RPG.Combat.Projectile
{
    public class FireBallProjectile : ProjectileBase
    {

        protected override void OnTriggerEnter(Collider other)
        {
            if (IsAlive == false) return;

            GameObject go = other.gameObject;

            if (go.IsSameLayer("Enemy"))
            {
                hitVfx.SetActive(true);
                var takedamageable = go.GetComponent<ITakeDamageable>();
                takedamageable?.TakeDamage(dmgInfo);
            }
        }
    }
}
