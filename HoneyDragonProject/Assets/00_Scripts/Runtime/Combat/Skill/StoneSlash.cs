using Cysharp.Threading.Tasks;
using RPG.Core.Data;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class StoneSlash : SpawnSkill
    {
        public override void SetData(SkillData data)
        {
            base.SetData(data);
        }

        protected override async UniTaskVoid Activate(Creature initiator)
        {
            for (int i = 0; i < spawnCount; ++i)
            {
                if (spawnObjects.TryGet(out var get) == false) break;
                // AudioSource soundComponent = get.GetComponent<AudioSource>();
                // AudioClip clip = soundComponent.clip;
                // soundComponent.PlayOneShot(clip);

                // Vector3 position = new Vector3(randX, 0f, randZ);
                // var enem = FindNearestEnemy(initiator, LayerMask.GetMask("Enemy"));
                Vector3 position = initiator.position + initiator.forward + Vector3.up * 0.5f;
                Vector3 spawnPos = position;
               
                get.Spawn(spawnPos, Quaternion.LookRotation(initiator.forward, initiator.up), Data);

                await UniTask.Delay(Data.SpawnRateMilliSecond * 2, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            }
        }

        public override async UniTaskVoid UseSkill()
        {
            while (true)
            {
                Activate(owner).Forget();
                await UniTask.Delay((int)(Data.CoolTime * 1000), false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            }
        }
    }
}
