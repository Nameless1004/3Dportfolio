using DamageNumbersPro;
using RPG.Core.Manager;
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
        public event Func<int, int> DamageHandle;

        public DamageNumber FloatingDamage;

        private void Awake()
        {
            CurrentHp = MaxHp;

            if(FloatingDamage != null)
            {
                FloatingDamage.enablePooling = true;
            }
        }

        public void SetHp(int hp)
        {
            CurrentHp = MaxHp = hp;
        }

        public void TakeDamage(DamageInfo damageInfo)
        {
            if(DamageHandle != null)
            {
                // 방어력 같은 거 처리
                damageInfo.Damage = DamageHandle.Invoke(damageInfo.Damage);
            }

            if (FloatingDamage != null)
            {
                FloatingDamage.Spawn(transform.position, damageInfo.Damage);
            }


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
