using RPG.Combat;
using RPG.Combat.Skill;
using RPG.Core.Data;
using RPG.Core.Item;
using RPG.Core.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Player : Creature
    {
        public PlayerStat Status { get; set; }
        private Dictionary<int, PlayerExpData> expTable;
        private Health health;

        public event Action<float> OnGetExp;
        public event Action OnLevelup;

        private void Awake()
        {
            collider = GetComponentInChildren<CapsuleCollider>();
            Status = new PlayerStat();
            health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            health.DamageHandle -= DamageHandle;
            health.DamageHandle += DamageHandle;
        }

        private void OnDisable()
        {
            health.DamageHandle -= DamageHandle;
        }

        // 플레이어가 데미지 받았을 때 계산해야할 곳 ex) 방어력
        private int DamageHandle(int damage)
        {
            float defense = Status.Defense / 100f;
            int result = damage - (int)(damage * defense);
            Logger.Log(result);
            return result;
        }

        public void InitializePlayer(PlayerData data, Dictionary<int, PlayerExpData> expTable)
        {
            Status.SetData(data);
            GetComponent<Health>().SetHp(data.Hp);
            this.expTable = expTable;
        }

        public void GetExp(int amount)
        {
            int resultExp = Status.Exp + amount;

            if (resultExp >= expTable[Status.Level].NeedExp) 
            {
                resultExp = resultExp -= expTable[Status.Level].NeedExp;
                // Levelup;
                OnLevelup?.Invoke();
            }
            Status.Exp = resultExp;
            OnGetExp.Invoke((float)Status.Exp / expTable[Status.Level].NeedExp);
        }

        public void GetGold(int amount)
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.IsSameLayer("Loot"))
            {
                Loot loot = other.GetComponent<Loot>();
                if(loot != null)
                {
                    loot.Get(this);
                }
            }
        }
    }
}
