using RPG.Core;
using RPG.Core.Creature;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class EnemyAIController : MonoBehaviour
    {
        public Creature Target;

        private EnemyData data;
        public NavMeshAgent Agent { get; private set; }
        private Health health;

        private Collider[] hit;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Target = GameObject.FindWithTag("Player").GetComponent<Creature>();
            health = GetComponent<Health>();
            data = GetComponent<Enemy>().Data;

            hit = new Collider[1];
        }

        private void Update()
        {
            
        }

        public void MoveTo(Vector3 destination)
        {
            Agent.isStopped = false;
            Agent.destination = destination;
        }

        public void StopAgent()
        {
            Agent.isStopped = true;
        }

        private void HitCheck()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, data.AttackRange, hit, LayerMask.GetMask("Player"));
            if (count != 0)
            {
                ITakeDamageable damageable = hit[0].GetComponent<ITakeDamageable>();
                DamageInfo dmgInfo = new DamageInfo { Damage = data.AttackPower, Sender = gameObject };
                damageable?.TakeDamage(dmgInfo);
            }
        }
    }
}
