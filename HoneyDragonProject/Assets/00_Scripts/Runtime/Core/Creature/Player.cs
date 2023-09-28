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
        private List<SkillBase> skillBases;

        private void Awake()
        {
            collider = GetComponentInChildren<CapsuleCollider>();
            Status = new PlayerStat();
            health = GetComponent<Health>();
            Debug.Log("Player Awake");
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

        private int DamageHandle(int damage)
        {
            return 0;
        }

        public void InitializePlayer(DataManager data)
        {
            Status.SetData(data.PlayerData);
            expTable = data.PlayerExpDataDict;
        }

        public void GetExp(int amount)
        {
            Debug.Log($"Player Get Exp : {amount}");
            int resultExp = Status.Exp + amount;
            if(resultExp > 0) 
            {
            }
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
                    Debug.Log("Loot");
                    loot.Get(this);
                }
            }
        }
    }
}
