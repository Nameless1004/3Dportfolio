using RPG.Combat;
using RPG.Core;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class EnemyAIController : MonoBehaviour
    {
        public Creature Target;

        public NavMeshAgent Agent { get; private set; }
        private Health health;

        private Collider[] hit;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();

            hit = new Collider[1];
        }

        public void SetTarget(Creature target) => Target = target;

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
          //  int count = Physics.OverlapSphereNonAlloc(transform.position, data.AttackRange, hit, LayerMask.GetMask("Player"));
            //if (count != 0)
            //{
            //    ITakeDamageable damageable = hit[0].GetComponent<ITakeDamageable>();
            //    DamageInfo dmgInfo = new DamageInfo { Damage = 5, Sender = gameObject };
            //    damageable?.TakeDamage(dmgInfo);
            //}
        }

        public void Die()
        {
            StopAgent();
            Target = null;
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
            //GetComponent<Animator>().SetTrigger("Dead");
        }
    }
}
