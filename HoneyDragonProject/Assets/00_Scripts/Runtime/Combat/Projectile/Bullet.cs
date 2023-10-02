using UnityEngine;

namespace RPG.Combat.Projectile
{
    public class Bullet : ProjectileBase
    {

        protected override void OnTriggerEnter(Collider other)
        {
            if (IsAlive == false) return;

            GameObject go = other.gameObject;

            if (go.IsSameLayer("Enemy") || go.IsSameLayer("Obstacle"))
            {
                var ps = hitVfx.GetComponentInChildren<ParticleSystem>(true);
                IsAlive = false;
                hitVfx.SetActive(true);
                rig.velocity = Vector3.zero;
                int durationMilliSec = (int)(ps.main.duration * 1000);
                DestroyParticle(durationMilliSec).Forget();

                if(go.IsSameLayer("Enemy"))
                {
                    var takedamageable = go.GetComponent<ITakeDamageable>();
                    takedamageable?.TakeDamage(dmgInfo);
                }
            }
        }
    }
}
