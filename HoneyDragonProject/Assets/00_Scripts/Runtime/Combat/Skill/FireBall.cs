using RPG.Combat.Projectile;
using RPG.Core.Data;
using RPG.Core.Manager;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class FireBall : ShootingSkill
    {
        public FireBall(SkillData data) : base(data)
        {     
        }

        public override ProjectilePooler CreatePool()
        {
            return new ProjectilePooler(Resources.Load<ProjectileBase>(projectilePrefabPath));
        }
    }
}
