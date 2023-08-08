using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public abstract class Actor : MonoBehaviour, INavMeshAgent
    {
        [HideInInspector] public int ActorId;
        [HideInInspector] public int ActorName;

        public abstract NavMeshAgent Agent { get; }
    }
}
