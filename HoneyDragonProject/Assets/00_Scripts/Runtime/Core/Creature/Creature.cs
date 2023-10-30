using UnityEngine;

namespace RPG.Core
{
    [RequireComponent(typeof(CapsuleCollider))]
    public abstract class Creature : MonoBehaviour
    {
        public Vector3 position { get { return transform.position; } set { transform.position = value; } }
        public Quaternion rotation { get { return transform.rotation; } set { transform.rotation = value; } }
        public Vector3 localScale { get { return transform.localScale; } set { transform.localScale = value; } }
        public Vector3 forward { get { return transform.forward; } set { transform.forward = value; } }
        public Vector3 right { get { return transform.right; } set { transform.right = value; } }
        public Vector3 up { get { return transform.up; } set { transform.up = value; } }

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
    }
}
