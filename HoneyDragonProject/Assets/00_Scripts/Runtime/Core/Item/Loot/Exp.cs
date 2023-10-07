using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

namespace RPG.Core.Item
{
    public class Exp : Loot
    {
        Transform model;
        CancellationToken destroyToken;
        protected bool followed = false;

        private void Awake()
        {
            model = transform.GetChild(0);
            destroyToken = this.GetCancellationTokenOnDestroy();
        }
        async UniTaskVoid LifeTimer()
        {
            float totalElapsedTime = 0f;
            while(true)
            {
                if (followed || isReleased) return;

                // 루트 생존시간이 지나면 풀에 반납
                if (totalElapsedTime > LOOT_LIFE_TIME)
                {
                    Logger.Log("Life time over Loot");
                    isReleased = true;
                    owner.Release(this);
                    return;
                }
                else
                {
                    totalElapsedTime += Time.deltaTime;
                }

                await UniTask.Yield(destroyToken);
            }
        }
        async UniTaskVoid Move()
        {
            float elapsedTime = 0f;
            float moveCycleTime = 1f;
            float startY = 0f;
            float endY = 1f;

            while(true)
            {
                if (followed || isReleased) return;

                if (elapsedTime > moveCycleTime)
                {
                    elapsedTime = 0f;
                }
                else
                {
                    float sin = Mathf.Sin(Mathf.Lerp(0, Mathf.PI, elapsedTime / moveCycleTime));
                    Vector3 newPosition = model.localPosition;
                    newPosition.y = Mathf.Lerp(startY, endY, sin);
                    model.localPosition = newPosition;
                    elapsedTime += Time.deltaTime;
                }

                await UniTask.Yield(destroyToken);
            }
        }

        public void ResetLoot()
        {
            model.localPosition = Vector3.zero;
            model.localScale = Vector3.one;
            followed = false;
            isReleased = false;
        }

        public override void Spawn(Vector3 position)
        {
            transform.position = position;
            Move().Forget();
            LifeTimer().Forget();
        }

        public override void Get(Player player)
        {
            FollowPlayer(player).Forget();
        }

        public async UniTaskVoid FollowPlayer(Player player)
        {
            if (isReleased) return;
            
            followed = true;

            float elapsedTime = 0f;
            float arriveTime = 1f;
            float height = 3f;
            Vector3 startPos = transform.position;
            Vector3 middlePos = transform.position + (player.position - startPos);
            middlePos.y += height;

            Vector3 newScale = Vector3.one;

            while (true)
            {
                if (destroyToken.IsCancellationRequested) return;
                if (isReleased) return;

                if (elapsedTime > arriveTime)
                {
                    isReleased = true;
                    player.GetExp(Amount);
                    owner.Release(this);
                    break;
                }
                else
                {
                    float ratio = elapsedTime / arriveTime;
                    float scaleRatio = Mathf.Lerp(1, 0, ratio);
                    newScale = new Vector3(scaleRatio, scaleRatio, scaleRatio);
                    model.localScale = newScale;

                    Vector3 p1 = Vector3.Lerp(startPos, middlePos, ratio);
                    Vector3 p2 = Vector3.Lerp(middlePos, player.position, ratio);
                    transform.position = Vector3.Lerp(p1, p2, ratio);
                    elapsedTime += Time.deltaTime;
                }

                await UniTask.Yield(PlayerLoopTiming.EarlyUpdate, destroyToken);
            }
        }

        public override void OnDestroyAction()
        {
           
        }

        public override void OnGetAction()
        {
            gameObject.SetActive(true);
            ResetLoot();
        }

        public override void OnReleaseAction()
        {
            gameObject.SetActive(false);
        }

        public override void SetPool(ObjectPool<Loot> owner)
        {
            this.owner = owner;
        }
    }
}
