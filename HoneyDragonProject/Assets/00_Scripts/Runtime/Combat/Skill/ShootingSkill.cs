using RPG.Combat.Projectile;
using RPG.Core;
using RPG.Core.Data;
using RPG.Util;

namespace RPG.Combat.Skill
{
    public abstract class ShootingSkill : ActiveSkill
    {
        public ShootingSkill(SkillData data) : base(data) 
        {
            projectilePrefabPath = data.PrefabPath;
            projectiles = CreatePool();
        }

        protected string projectilePrefabPath;
        protected ObjectPooler<ProjectileBase> projectiles;

        
        public virtual ObjectPooler<ProjectileBase> CreatePool()
        {
            return new ObjectPooler<ProjectileBase>(Utility.ResourceLoad<ProjectileBase>(projectilePrefabPath));
        }

    }
}
