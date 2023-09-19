using UnityEngine;

namespace RPG.Core
{
    [RequireComponent(typeof(CapsuleCollider))]
    public abstract class Creature : MonoBehaviour
    {
        [HideInInspector] public Vector3 position => transform.position;
        [HideInInspector] public Quaternion rotation => transform.rotation;
        [HideInInspector] public Vector3 localScale => transform.localScale;
        [HideInInspector] public Vector3 forward => transform.forward;
        [HideInInspector] public Vector3 right => transform.right;

        [HideInInspector] public Vector3 center { get
            {
                if(collider is null)
                {
                    collider = GetComponentInChildren<CapsuleCollider>();
                }

                return collider.bounds.center;
            }
        }
        
        protected CapsuleCollider collider;


        private void Awake()
        {
            collider = GetComponentInChildren<CapsuleCollider>();
        }
    }
}
