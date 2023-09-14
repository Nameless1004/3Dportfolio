using RPG.Combat.Projectile;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class ShootingSkill : ActiveSkill
    {
        public ShootingSkill() 
        {
            projectiles = new ProjectilePooler(Resources.Load<ProjectileBase>("test"));
        }

        ProjectilePooler projectiles;


        public override void Activate(Transform initiator)
        {
            var get = projectiles.Pool.Get();
            get.Fire(new DamageInfo() { Damage = 5}, initiator.position, initiator.position, 5f, initiator);
        }
    }
}
