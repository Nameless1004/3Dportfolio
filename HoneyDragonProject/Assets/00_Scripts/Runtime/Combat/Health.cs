using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour, ITakeDamageable
    {
        public int MaxHp;
        public int CurrentHp;
        private float ratio;

        public event Action<float> OnHealthChanged;
        public event Action OnHit;
        public event Action OnDie;

        private void Awake()
        {
            CurrentHp = MaxHp;
        }

        public void SetHp(int hp)
        {
            CurrentHp = MaxHp = hp;
        }

        public void TakeDamage(DamageInfo damageInfo)
        {
            int damagedHp = Mathf.Clamp(CurrentHp - damageInfo.Damage, 0, MaxHp);
            if(damagedHp <= 0)
            {
                // DO SOMETHING
                OnDie?.Invoke();
            }

            ratio = CurrentHp / MaxHp;
            OnHealthChanged?.Invoke(ratio);
            CurrentHp = damagedHp;
            OnHit?.Invoke();
        }
    }
}
