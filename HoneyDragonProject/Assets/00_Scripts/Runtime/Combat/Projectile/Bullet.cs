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
            transform.position = calculatedPosition;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
        }
    }
}
