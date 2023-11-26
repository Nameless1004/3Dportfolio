using RPG.Combat;
using RPG.Core;
using RPG.Core.Manager;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class NormalMonsterAIController : MonoBehaviour
    {
        private float moveSpeed;
        private GridController gridController;
        private NormalMonster enemy;
        public Animator Animator { get; private set; }
        public Rigidbody RigidBody { get; private set; }
        public Creature Target;
        protected Health health;
        protected new Collider collider;

        private Vector3 direction;

        private void Awake()
        {
            enemy = GetComponent<NormalMonster>();
            health = GetComponent<Health>();
            collider = GetComponentInChildren<Collider>();
            RigidBody = GetComponentInChildren<Rigidbody>();
            Animator = GetComponentInChildren<Animator>();

            gridController = Managers.Instance.Stage.CurrentStage.Grid;
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
            if(direction == Vector3.zero)
            {
                direction = (Target.position - transform.position).normalized;
            }
            RigidBody.position += direction * enemy.Data.MoveSpeed * Time.deltaTime;
        }

        float currentVelocity;
        float smoothTime = 0.2f;
        private void Rotate()
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        private void Update()
        {
            if (Target == null) return;

            MoveUseFlowField();
            Rotate();
        }

        public void OnDie()
        {
            RigidBody.useGravity = false;
            RigidBody.velocity = Vector3.zero;
            collider.enabled = false;
            Animator.ResetTrigger("Die");
            Animator.SetTrigger("Die");
            Target = null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (Target == null) return;

            if (collision.gameObject.IsSameLayer("Player"))
            {
                enemy.Owner.Release(enemy);
                ITakeDamageable damageable = collision.gameObject.GetComponent<ITakeDamageable>();
                damageable?.TakeDamage(new DamageInfo(null, enemy.Data.AttackPower));
            }
        }

        public void Init()
        {
            collider.enabled = true;
            RigidBody.useGravity = true;
            Target = Managers.Instance.Game.CurrentPlayer;
        }
    }
}
