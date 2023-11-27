using RPG.Combat;
using RPG.Core.Data;
using RPG.Core.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Core.UI
{
    public class BossLimitTimer : MonoBehaviour
    {
        public TMP_Text TimeText;
        private bool timerStart;
        private float limitTime;
        private Action OnTimeOver;
        private StageData currentStageData;

        private void Awake()
        {
            gameObject.SetActive(false);
            Managers.Instance.Game.GameScene.MonsterSpawner.OnBossSpawned += Show;
        }

        private void Update()
        {
            if (timerStart == true)
            {
                limitTime -= Time.deltaTime;
                if(limitTime <= 0)
                {
                    OnTimeOver?.Invoke();
                    Stop();
                    return;
                }
                TimeSpan span = TimeSpan.FromSeconds(limitTime);
                TimeText.text = $"{span.Minutes:D2} : {span.Seconds:D2}";
            }
        }

        public void Show()
        {
            SetTimer();
            var currentBoss = Managers.Instance.Game.GameScene.MonsterSpawner.CurrentBoss;
            if (currentBoss != null)
            {
                currentBoss.GetComponent<Health>().OnDie += Stop;
            }
        }

        public void Stop()
        {
            limitTime = 0;
            timerStart = false;
            TimeText.text = "";
            gameObject.SetActive(false);
            return;
        }

        public void SetTimer()
        {
            gameObject.SetActive(true);
            timerStart = true;
            limitTime = Managers.Instance.Stage.CurrentStageData.BossBattleLimitTime;
            TimeSpan span = TimeSpan.FromSeconds(limitTime);
            TimeText.text = $"{span.Minutes:D2} : {span.Seconds:D2}";

            OnTimeOver = ()=>
            {
                var takedamageable = Managers.Instance.Game.CurrentPlayer.GetComponent<ITakeDamageable>();
                takedamageable.TakeDamage(new DamageInfo(null, 9999));
            };
        }
    }
}
