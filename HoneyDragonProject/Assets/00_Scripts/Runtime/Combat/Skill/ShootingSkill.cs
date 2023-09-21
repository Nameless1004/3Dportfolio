using RPG.Combat.Projectile;
using RPG.Core;
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
        protected ObjectPooler<ProjectileBase> projectiles;

        public abstract ObjectPooler<ProjectileBase> CreatePool();
    }
}
