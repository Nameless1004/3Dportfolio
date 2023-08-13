using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float smoothTime = 0.05f;
        [SerializeField] private bool useGravity = true;
        [SerializeField] private float gravity = 9.8f;

        private Vector3 direction;
        private Vector3 moveVec;
        private Vector3 gravityVec;
        private float currentVelocity;
        PlayerInput input;
        Animator animator;
        CharacterController characterController;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
            animator = GetComponentInChildren<Animator>();
            characterController = GetComponent<CharacterController>();
            gravityVec = new Vector3(0, -gravity, 0);
        }

        private void Update()
        {
            UpdateDirection();
            UpdateAnimationParameter();
            UpdateGravity();
            if (input.MoveInput.sqrMagnitude == 0) return;
            Rotate();
            Move();
        }

        private void UpdateGravity()
        {
            if (useGravity && characterController.isGrounded == false)
            {
                characterController.Move(gravityVec);
            }
        }

        private void UpdateAnimationParameter()
        {
            float forwardSpeed = transform.InverseTransformDirection(direction).z;
            animator.SetFloat("ForwardSpeed", Mathf.Abs(forwardSpeed));
        }

        private void UpdateDirection()
        {
            direction = input.MoveInput.normalized;
        }

        private void Move()
        {
            moveVec = (moveSpeed * Time.deltaTime) * direction;
            characterController.Move(moveVec);
        }

        private void Rotate()
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

    }
}
