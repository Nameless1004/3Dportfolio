using UnityEngine.AI;

namespace RPG.Core
{
    public class Player : Actor
    {
        private NavMeshAgent agent;

        public override NavMeshAgent Agent => agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }
}
