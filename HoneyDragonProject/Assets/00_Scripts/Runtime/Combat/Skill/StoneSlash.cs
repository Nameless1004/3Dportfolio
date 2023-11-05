using Cysharp.Threading.Tasks;
using RPG.Core.Data;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core.Manager;

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

                if (i % 2 == 0)
                {
                    Vector3 forwardPosition = initiator.position + initiator.forward + Vector3.up * 0.5f;
                    get.Spawn(forwardPosition, Quaternion.LookRotation(initiator.forward, initiator.up), Data);
                }
                else
                {
                    Vector3 backwardPosition = initiator.position + (-initiator.forward) + Vector3.up * 0.5f;
                    get.Spawn(backwardPosition, Quaternion.LookRotation(-initiator.forward, initiator.up), Data);
                }

                Managers.Instance.Sound.PlaySound(SoundType.Effect, activeSoundResourcePath);

                await UniTask.Delay(Data.SpawnRateMilliSecond, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
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
