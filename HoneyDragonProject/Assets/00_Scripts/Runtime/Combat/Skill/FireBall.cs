﻿using Cysharp.Threading.Tasks;
using RPG.Combat.Projectile;
using RPG.Core;
using RPG.Core.Data;
using RPG.Core.Manager;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class FireBall : ShootingSkill
    {
        public FireBall(SkillData data) : base(data)
        {
            CurrentLevel = 1;
        }

        public override async UniTaskVoid Activate(Creature initiator, CancellationToken token)
        {
            for (int i = 0; i < 5; ++i)
            {
                if (projectiles.TryGet(out var get) == false) break;
                float randZ = Random.Range(-1f, 1f);
                float randX = Random.Range(-1f, 1f);
                Vector3 dir = new Vector3(randX, 0f, randZ).normalized;
                var enem = FindNearestEnemy(initiator, LayerMask.GetMask("Enemy"));
                if (enem != null)
                {
                    dir = (enem.transform.position - initiator.position).normalized;
                }
                get.Fire(new DamageInfo(null, Random.Range(Data.MinDamage, Data.MaxDamage + 1), new KnockbackInfo(dir, 5f)), initiator.center, dir, Data.ProjectileSpeed, initiator);

                await UniTask.Delay(Data.SpawnRateMilliSecond, false, PlayerLoopTiming.Update, token);
            }
        }

        public override void Levelup()
        {
            var data = Managers.Instance.Skill.LevelupSkill(Id, CurrentLevel);
            if (data is not null)
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
