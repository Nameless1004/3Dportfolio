using Cysharp.Threading.Tasks;
using RPG.Core;
using RPG.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class FireStone : SpawnSkill
    {

        public async override UniTaskVoid UseSkill()
        {
            while (true)
            {
                Activate(owner).Forget();
                await UniTask.Delay((int)(Data.CoolTime * 1000), false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            }
        }

        protected async override UniTaskVoid Activate(Creature initiator)
        {
            for (int i = 0; i < spawnCount; ++i)
            {
                if (spawnObjects.TryGet(out var get) == false) break;
                // AudioSource soundComponent = get.GetComponent<AudioSource>();
                // AudioClip clip = soundComponent.clip;
                // soundComponent.PlayOneShot(clip);

                Vector3 spawnPos = initiator.position + Vector3.up * 0.5f;

                get.Spawn(spawnPos, Quaternion.identity, Data);

                await UniTask.Delay(Data.SpawnRateMilliSecond, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            }
        }
    }
}
