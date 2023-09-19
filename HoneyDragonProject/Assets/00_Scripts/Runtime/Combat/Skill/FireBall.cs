using RPG.Combat.Projectile;
using RPG.Core;
using RPG.Core.Data;
using RPG.Core.Manager;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class FireBall : ShootingSkill
    {
        public FireBall(SkillData data) : base(data)
        {
            CurrentLevel = 1;
        }

        public override ProjectilePooler CreatePool()
        {
            return new ProjectilePooler(Resources.Load<ProjectileBase>(projectilePrefabPath));
        }

        public override void Activate(Creature initiator)
        {
            for(int i = 0; i < Data.SpawnCount; ++i)
            {
                var get = projectiles.Pool.Get();
                float randZ = Random.Range(-1f, 1f);
                float randX = Random.Range(-1f, 1f);
                Vector3 dir = new Vector3(randX, 0f, randZ).normalized;
                get.Fire(new DamageInfo(null, 5, new KnockbackInfo(dir, 5f)), initiator.center, dir, Data.ProjectileSpeed, initiator);
            }
        }

        public override void Levelup()
        {
            var data = Managers.Instance.Skill.LevelupSkill(Id, CurrentLevel);
            if(data is not null)
            {
                this.Data = data;
                CurrentLevel++;
            }
            else
            {
                // 맥스레벨

            }
        }
    }
}
