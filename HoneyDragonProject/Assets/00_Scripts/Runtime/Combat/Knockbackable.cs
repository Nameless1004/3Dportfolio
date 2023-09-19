using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class Knockbackable : MonoBehaviour
    {
        Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Knockback(KnockbackInfo info)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(info.forceDir * info.force, ForceMode.Impulse);
        }
        public void KnockbackExplosion(KnockbackInfo info)
        {
            rigidbody.AddExplosionForce(info.force, info.point, info.radius);
        }
    }
}
