using UnityEngine;

namespace RPG.Combat
{
    public struct KnockbackInfo
    {
        public enum KnockbackType
        {
            Normal,
            Explosion
        }

        public KnockbackInfo(Vector3 forceDir, float force)
        {
            this.type = KnockbackType.Normal;
            this.forceDir = forceDir;
            this.force = force;
            this.point = Vector3.zero;
            this.radius = 0;
        }

        public KnockbackInfo(Vector3 point, float force, float radius)
        {
            this.type = KnockbackType.Explosion;
            this.point = point;
            this.force = force;
            this.radius = radius;
            this.forceDir = Vector3.zero;
        }

        public KnockbackType type;
        public Vector3 point;
        public Vector3 forceDir;
        public float force;
        public float radius;
    }
    public struct DamageInfo
    {
        public DamageInfo(GameObject sender, int damage) 
        {
            IsKnockback = false;
            this.Sender = sender;
            this.Damage = damage;
            this.knockbackInfo = new KnockbackInfo();
        }

        public DamageInfo(GameObject sender, int damage, KnockbackInfo knockbackInfo)
        {
            IsKnockback = true;
            this.Sender = sender;
            this.Damage = damage;
            this.knockbackInfo = knockbackInfo;
        }

        public GameObject Sender;
        public int Damage;
        public bool IsKnockback;
        public KnockbackInfo knockbackInfo;
    }
}
