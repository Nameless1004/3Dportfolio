using Cysharp.Threading.Tasks;
using RPG.Combat;
using System.Collections.Generic;
using System.Linq;
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
        Rigidbody rig;
        PlayerInput input;
        Animator animator;

        public bool initialized = false;

        // 플레이어 모델 생성해주고 바인딩
        public void Init()
        {
            rig = GetComponent<Rigidbody>();
            input = GetComponent<PlayerInput>();
            animator = GetComponentInChildren<Animator>();

            initialized = true;
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
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            return Quaternion.Euler(0, angle, 0);
        }

    }
}
