using RPG.Combat.Projectile;
using RPG.Core;
using RPG.Core.Data;
using RPG.Core.Manager;
using System.Collections;
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
            return new ObjectPooler<ProjectileBase>(Resources.Load<ProjectileBase>(projectilePrefabPath));
        }

        public override IEnumerator Activate(Creature initiator)
        {
            for(int i = 0; i < 5; ++i)
            {
                var get = projectiles.Get<Bullet>();
                float randZ = Random.Range(-1f, 1f);
                float randX = Random.Range(-1f, 1f);
                Vector3 dir = new Vector3(randX, 0f, randZ).normalized;
                var enem = FindNearestEnemy(initiator, LayerMask.GetMask("Enemy"));
                if (enem is not null)
                {
                    dir = (enem.transform.position - initiator.position).normalized;
                }
                get.Fire(new DamageInfo(null, Random.Range(Data.MinDamage, Data.MaxDamage + 1), new KnockbackInfo(dir, 5f)), initiator.center, dir, Data.ProjectileSpeed, initiator);
                yield return new WaitForSeconds(0.1f);
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
