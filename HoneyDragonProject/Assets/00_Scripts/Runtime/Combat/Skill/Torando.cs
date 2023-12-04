using Cysharp.Threading.Tasks;
using RPG.Core.Data;
using RPG.Util;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class Torando : SpawnObject
    {
        private Vector3 direction;
        private Transform target;
        new private CapsuleCollider collider;

        // 스폰 오브젝트의 토큰과 OnDestroy토큰이 링크된 토큰
        private CancellationToken linkedToken;

        public override void Spawn(Vector3 position, SkillData data)
        {
            base.Spawn(position, data);
            if (collider == null)
            {
                collider = GetComponent<CapsuleCollider>();
            }
            linkedToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource.Token, this.GetCancellationTokenOnDestroy()).Token;
            
            TargetUpdate().Forget();
            MoveToNearEnemy().Forget();
            HitCheck().Forget();
        }

        private async UniTaskVoid MoveToNearEnemy()
        {
            while (true)
            {
                if (target == null)
                {
                    direction.SetRandomDirectionXZ();
                }
                else
                {
                    direction = (target.position - transform.position).normalized;
                    direction.y = 0f;
                }

                transform.position += direction * Time.deltaTime * speed;

                await UniTask.Yield(linkedToken);
            }
        }

        private async UniTaskVoid HitCheck()
        {
            while (true)
            {
                var hits = Physics.OverlapSphere(transform.position, collider.radius, LayerMask.GetMask("Enemy"));
                for (int i = 0; i < hits.Length; ++i)
                {
                    var takedamageable = hits[i].GetComponent<ITakeDamageable>();
                    takedamageable?.TakeDamage(new DamageInfo()
                    {
                        Damage = Random.Range(minDamage, maxDamage + 1),
                        IsKnockback = true,
                        knockbackInfo = new KnockbackInfo()
                        {
                            force = 10f,
                            type = KnockbackInfo.KnockbackType.Explosion,
                            point = transform.position,
                            radius = 3f
                        }
                    });
                }
                await UniTask.Delay(400, false, PlayerLoopTiming.Update, linkedToken);
            }
        }

        private async UniTaskVoid TargetUpdate()
        {
            while (true)
            {
                Transform nearestTarget = Utility.FindNearestObject(transform, 50f, LayerMask.GetMask("Enemy"));
                if (nearestTarget != null)
                {
                    target = nearestTarget;
                    await UniTask.Delay(1000, false, PlayerLoopTiming.Update, linkedToken);
                }
                else
                {
                    await UniTask.Yield(linkedToken);
                }
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            return;
        }
    }
}
