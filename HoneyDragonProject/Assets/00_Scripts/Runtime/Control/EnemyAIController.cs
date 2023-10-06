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
        public Rigidbody RigidBody { get; private set; }
        public Creature Target;
        private Health health;
        private new Collider collider;

        private Vector3 direction;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            health = GetComponent<Health>();
            collider = GetComponentInChildren<Collider>();
            RigidBody = GetComponentInChildren<Rigidbody>();
            Animator = GetComponentInChildren<Animator>();

            // Test
            gridController = FindObjectOfType<GridController>();
        }

        private void OnEnable()
        {
            health.OnDie -= OnDie;
            health.OnDie += OnDie;
        }

        private void OnDisable()
        {
            health.OnDie -= OnDie;
        }

        public void SetTarget(Creature target)
        {
            Target = target;
            collider.enabled = true;
            Animator.ResetTrigger("Chase");
            Animator.SetTrigger("Chase");
        }

        private void MoveUseFlowField()
        {
            Cell cellBlow = gridController.CurFlowField.GetCellFromWorldPos(transform.position);
            direction = new Vector3(cellBlow.BestDirection.Vector.x, 0, cellBlow.BestDirection.Vector.y);
            RigidBody.position += direction * 2f * Time.deltaTime;
            // RigidBody.velocity = direction * 2f;
            // transform.forward = direction;
            Debug.DrawRay(transform.position, direction * 5f, Color.yellow);
        }

        float currentVelocity;
        float smoothTime = 0.2f;
        private void Rotate()
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        public void StopAgent()
        {
            //Agent.isStopped = true;
            //RigidBody.velocity = Vector3.zero;
        }

        private void Update()
        {
            if (Target == null) return;

            Debug.DrawRay(transform.position, direction * 5f, Color.yellow);
            //Vector3 dir = Target.position - transform.position;
            //dir = dir.normalized;
            //transform.forward = dir;
            //RigidBody.Move(RigidBody.position + dir * Time.deltaTime * 2f, Quaternion.identity);

            MoveUseFlowField();
            Rotate();
        }

        public void OnDie()
        {
            collider.enabled = false;
            Animator.ResetTrigger("Die");
            Animator.SetTrigger("Die");
            StopAgent();
            Target = null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (Target == null) return;

            if (collision.gameObject.IsSameLayer("Player"))
            {
                enemy.Owner.Release(enemy);
                ITakeDamageable damageable = collision.gameObject.GetComponent<ITakeDamageable>();
                damageable?.TakeDamage(new DamageInfo(null, 5));
            }
        }
    }
}
