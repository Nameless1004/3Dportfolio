using RPG.Combat.Projectile;
using RPG.Core;
using RPG.Core.Data;
using RPG.Util;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public abstract class ShootingSkill : ActiveSkill
    {
        [SerializeField]
        protected ProjectileBase prefab;
        protected ObjectPooler<ProjectileBase> projectiles;

        public override void SetData(SkillData data)
        {
            base.SetData(data);
            projectiles = CreatePool();
            activeSoundResourcePath = $"Sound/Skill/{data.Id}";
        }

        public virtual ObjectPooler<ProjectileBase> CreatePool()
        {
            return new ObjectPooler<ProjectileBase>(prefab);
        }

    }
}
