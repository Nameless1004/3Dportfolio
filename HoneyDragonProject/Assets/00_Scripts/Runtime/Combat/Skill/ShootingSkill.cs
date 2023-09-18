using RPG.Combat.Projectile;
using RPG.Core.Data;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public abstract class ShootingSkill : ActiveSkill
    {
        public ShootingSkill(SkillData data) : base(data) 
        {
            projectilePrefabPath = data.ProjectilePath;
            projectiles = CreatePool();
        }

        protected string projectilePrefabPath;
        ProjectilePooler projectiles;

        public abstract ProjectilePooler CreatePool();


        public override void Activate(Transform initiator)
        {
            var get = projectiles.Pool.Get();
            get.Fire(new DamageInfo() { Damage = 5}, initiator.position, initiator.forward, Data.ProjectileSpeed, initiator);
        }
    }
}
