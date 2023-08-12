﻿using UnityEngine;

namespace RPG.Core.Character
{
    public class Health : MonoBehaviour, ITakeDamageable
    {
        public int MaxHp;
        public int CurrentHp;

        private void Awake()
        {
            CurrentHp = MaxHp;
        }

        public void TakeDamage(DamageInfo damageInfo)
        {
            int damagedHp = Mathf.Clamp(CurrentHp - damageInfo.Damage, 0, MaxHp);
            if(damagedHp <= 0)
            {
                // DO SOMETHING
            }
            CurrentHp = damagedHp;
        }
    }
}
