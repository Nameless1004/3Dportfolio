using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float smoothTime = 0.05f;

        private Vector3 direction;
        private float currentVelocity;
        PlayerInput input;
        Animator animator;
        CharacterController characterController;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
            animator = GetComponentInChildren<Animator>();
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            UpdateDirection();
            AnimationUpdate();
            if (input.MoveInput.sqrMagnitude == 0) return;
            Rotate();
            Move();
        }

        private void AnimationUpdate()
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
            characterController.Move(moveSpeed * Time.deltaTime * direction);
        }

        private void Rotate()
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

    }
}
