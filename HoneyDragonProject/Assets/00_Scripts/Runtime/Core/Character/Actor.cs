using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core.Character
{
    public abstract class Actor : MonoBehaviour
    {
        [HideInInspector] public int ActorId;
        [HideInInspector] public int ActorName;
    }
}
