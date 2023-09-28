using Cysharp.Threading.Tasks;
using RPG.Core;
using RPG.Core.Data;
using System.Threading;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class LightningBolt : SpawnSkill
    {
        public override void SetData(SkillData data)
        {
            base.SetData(data);
        }

        protected override async UniTaskVoid Activate(Creature initiator)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (spawnObjects.TryGet(out var get) == false) break;

                float randZ = Random.Range(-1f, 1f);
                float randX = Random.Range(-1f, 1f);
                // Vector3 position = new Vector3(randX, 0f, randZ);
                // var enem = FindNearestEnemy(initiator, LayerMask.GetMask("Enemy"));
                Vector2 position = (Random.insideUnitCircle * 10f);
                Vector3 spawnPos = initiator.position + new Vector3(position.x, 0f, position.y);
                spawnPos.y = 0f;
                get.Spawn(spawnPos, 1f);

                await UniTask.Delay(Data.SpawnRateMilliSecond * 2, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            }
        }


        public override void Levelup()
        {
        }

        public override async UniTaskVoid UseSkill()
        {
            while(true)
            {
                Activate(owner).Forget();
                await UniTask.Delay((int)(Data.CoolTime * 1000), false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            }
        }
    }
}
