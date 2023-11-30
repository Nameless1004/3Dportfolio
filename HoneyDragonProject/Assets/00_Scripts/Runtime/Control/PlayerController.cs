using Cysharp.Threading.Tasks;
using RPG.Combat;
using RPG.Core.Data;
using RPG.Core.Manager;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float smoothTime = 0.01f;

        private Vector3 direction;
        private Vector3 moveVec;
        private float currentVelocity;
        private PlayerSkillController skillController;
        private Rigidbody rig;
        private PlayerInput input;
        private Animator animator;
        private Collider collider;
        private Health health;

        public bool initialized = false;

        // 플레이어 모델 생성해주고 바인딩
        public void Init(PlayerData playerData)
        {
            skillController = gameObject.AddComponent<PlayerSkillController>();
            rig = GetComponent<Rigidbody>();
            input = GetComponent<PlayerInput>();
            animator = GetComponentInChildren<Animator>();
            collider = GetComponentInChildren<Collider>();
            health = GetComponent<Health>();

            moveSpeed = playerData.MoveSpeed;

            initialized = true;

            health.OnDie -= OnPlayerDie;
            health.OnDie += OnPlayerDie;

            Managers.Instance.Game.GameScene.OnGameClear -= OnGameClear;
            Managers.Instance.Game.GameScene.OnGameClear += OnGameClear;
        }

        private void OnGameClear()
        {
            animator.SetTrigger("Victory");
            PlayerDieProcess();
        }

        private void OnPlayerDie()
        {
            animator.SetTrigger("Die");
            PlayerDieProcess();
        }

        private void PlayerDieProcess()
        {
            rig.velocity = Vector3.zero;
            collider.enabled = false;
            skillController.DestroySkill();
            Destroy(skillController);
            Destroy(this);
        }

        private void Update()
        {
            if (initialized == false) return;
            UpdateDirection();
            UpdateAnimationParameter();
            Move();
        }

        private void UpdateAnimationParameter()
        {
            float forwardSpeed = transform.InverseTransformDirection(direction).z;
            animator.SetFloat("ForwardSpeed", Mathf.Abs(forwardSpeed));
        }

        private void UpdateDirection()
        {
            direction = input.MoveInput;
        }

        private void Move()
        {
            moveVec = moveSpeed * direction;
            rig.velocity = moveVec;

            if (input.IsInput)
            {
                transform.rotation = Rotate();
            }
        }

        private Quaternion Rotate()
        {
            if(direction == Vector3.zero) return Quaternion.identity;

            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            return Quaternion.Euler(0, angle, 0);
        }

    }
}
