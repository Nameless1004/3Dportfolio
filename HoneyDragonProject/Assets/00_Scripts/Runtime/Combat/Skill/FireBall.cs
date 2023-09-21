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

        public override ObjectPooler<ProjectileBase> CreatePool()
        {
            return new ObjectPooler<ProjectileBase>(Resources.Load<ProjectileBase>(projectilePrefabPath), Managers.Instance.Game.PoolTransform);
        }

        public override void Activate(Creature initiator)
        {
            for(int i = 0; i < Data.SpawnCount; ++i)
            {
                var get = projectiles.Get<Bullet>();
                float randZ = Random.Range(-1f, 1f);
                float randX = Random.Range(-1f, 1f);
                Vector3 dir = new Vector3(randX, 0f, randZ).normalized;
                var enem = GameObject.FindWithTag("Enemy");
                dir = (enem.transform.position - initiator.position).normalized;
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
