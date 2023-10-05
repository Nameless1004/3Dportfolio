using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float smoothTime = 0.05f;

        private Vector3 direction;
        private Vector3 moveVec;
        private float currentVelocity;
        Rigidbody rig;
        PlayerInput input;
        Animator animator;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            input = GetComponent<PlayerInput>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            UpdateDirection();
            UpdateAnimationParameter();
            if (input.MoveInput.sqrMagnitude == 0) return;
            Move();
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
            moveVec = rig.position + (moveSpeed * Time.deltaTime) * direction;
            rig.Move(moveVec, Rotate());
        }

        private Quaternion Rotate()
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            return Quaternion.Euler(0, angle, 0);
        }

    }
}
