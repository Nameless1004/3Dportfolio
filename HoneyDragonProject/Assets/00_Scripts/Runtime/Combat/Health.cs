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

            ratio = CurrentHp / MaxHp;
            OnHealthChanged?.Invoke(ratio);
            CurrentHp = damagedHp;
            OnHit?.Invoke();

            if (damagedHp <= 0)
            {
                // DO SOMETHING
                OnDie?.Invoke();
                return;
            }
            
            if(damageInfo.IsKnockback)
            {
                var knockbackable = GetComponent<Knockbackable>();
                knockbackable?.Knockback(damageInfo.knockbackInfo);
            }
        }
    }
}
