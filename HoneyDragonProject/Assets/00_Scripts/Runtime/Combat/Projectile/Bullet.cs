using UnityEngine;

namespace RPG.Combat.Projectile
{
    public class Bullet : ProjectileBase
    {
        protected override void CalculatePosition()
        {
            calculatedPosition = transform.position + direction * (Time.deltaTime * speed);
        }

        protected override void ApplyPosition()
        {
            rig.position = calculatedPosition;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (IsAlive == false) return;

            GameObject go = other.gameObject;

            if (go.IsSameLayer("Enemy") || go.IsSameLayer("Obstacle"))
            {
                // 적이면 
                Debug.Log("적이랑 충돌됨");
                Debug.Log($"{other.name}");
                var ps = hitVfx.GetComponentInChildren<ParticleSystem>(true);
                IsAlive = false;
                hitVfx.SetActive(true);
                StartCoroutine(DestroyParticle(ps.main.duration));

                if(go.IsSameLayer("Enemy"))
                {
                    var takedamageable = go.GetComponent<ITakeDamageable>();
                    takedamageable?.TakeDamage(dmgInfo);
                }
            }
        }
    }
}
