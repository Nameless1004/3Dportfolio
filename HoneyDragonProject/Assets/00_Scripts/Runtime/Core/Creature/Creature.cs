using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core.Creature
{
    public abstract class Creature : MonoBehaviour
    {
        [HideInInspector] public Vector3 position => transform.position;
        [HideInInspector] public Quaternion rotation => transform.rotation;
        [HideInInspector] public Vector3 localScale => transform.localScale;
    }
}
