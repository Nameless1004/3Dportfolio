using RPG.Combat;
using RPG.Core;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] GridController gridController;
        private Enemy enemy;
        public Animator Animator { get; private set; }
        public Rigidbody RigidBody{ get; private set; }
        public Creature Target;
        public NavMeshAgent Agent { get; private set; }
        private new Collider collider;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            enemy = GetComponent<Enemy>();
            collider = GetComponentInChildren<Collider>();
            RigidBody = GetComponentInChildren<Rigidbody>();    
            Animator = GetComponentInChildren<Animator>();

            // Test
            gridController = FindObjectOfType<GridController>();
        }

        public void SetTarget(Creature target)
        {
            Target = target;
            MoveTo(target.position);
            collider.enabled = true;
            Animator.ResetTrigger("Chase");
            Animator.SetTrigger("Chase");
            Debug.Log("SetTarget");
        }

        public void MoveTo(Vector3 destination)
        {
            Agent.isStopped = false;
            Agent.destination = destination;
        }

        private void MoveUseFlowField()
        {
            Cell cellBlow = gridController.CurFlowField.GetCellFromWorldPos(transform.position);
            Vector3 moveDir = new Vector3(cellBlow.BestDirection.Vector.x, 0, cellBlow.BestDirection.Vector.y);
            RigidBody.velocity = moveDir * 2f;
            transform.forward = moveDir;
            Debug.DrawRay(transform.position, moveDir * 5f, Color.yellow);
        }

        public void StopAgent()
        {
            Agent.isStopped = true;
        }

        private void Update()
        {
            if (Target == null) return;

            // MoveTo(Target.position);
            MoveUseFlowField();
        }

        public void OnDie()
        {
            collider.enabled = false;
            Animator.ResetTrigger("Die");
            Animator.SetTrigger("Die");
            StopAgent();
            Target = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Target == null) return;

            if (other.gameObject.CompareTag("Player"))
            {
                enemy.Owner.Release(enemy);
                ITakeDamageable damageable = other.gameObject.GetComponent<ITakeDamageable>();
                damageable?.TakeDamage(new DamageInfo(null, 5));
            }
        }

    }
}
