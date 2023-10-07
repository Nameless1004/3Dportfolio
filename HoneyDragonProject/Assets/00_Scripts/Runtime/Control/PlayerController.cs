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

        [SerializeField] private Material HitMat;

        private Vector3 direction;
        private Vector3 moveVec;
        private float currentVelocity;
        Rigidbody rig;
        PlayerInput input;
        Animator animator;

        private Dictionary<SkinnedMeshRenderer, Material> rendererDefaultMaterialDict = new Dictionary<SkinnedMeshRenderer, Material>();
        private List<SkinnedMeshRenderer> renderers;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            input = GetComponent<PlayerInput>();
            animator = GetComponentInChildren<Animator>();

            renderers = GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
            foreach(var renderer in renderers)
            {
                rendererDefaultMaterialDict.Add(renderer, renderer.material);
            }
        }

        private void Update()
        {
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
            direction = input.MoveInput.normalized;
        }

        private void Move()
        {
            moveVec = moveSpeed * direction;
            rig.velocity = moveVec;

            if (input.IsInput)
            {
                rig.rotation = Rotate();
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
