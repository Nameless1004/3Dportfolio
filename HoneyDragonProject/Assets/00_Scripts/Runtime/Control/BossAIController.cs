using RPG.Combat;
using RPG.Combat.AI.BehaviourTree;
using RPG.Core;
using RPG.Core.Manager;
using System.Linq;
using UnityEngine;

namespace RPG.Control
{
    public class BossAIController : MonoBehaviour
    {
        [SerializeField] private Boss owner;
        [SerializeField] private BehaviourTreeRunner btRunner;
        private Rigidbody rigidBody;
        private Creature target;
        private GridController gridController;
        private Vector3 direction;
        public bool IsAttackEnd = true;

        private void Awake()
        {
            owner = GetComponent<Boss>();
            rigidBody = GetComponent<Rigidbody>();
            gridController = Managers.Instance.Stage.CurrentStage.Grid;
            target = Managers.Instance.Game.CurrentPlayer;
            btRunner.TreeInit(gameObject);
            IsAttackEnd = true;
        }

        private void Start()
        {
            btRunner.Run();
        }

        public void Move()
        {
            if (target == null) return;
            MoveUseFlowField();
            Rotate();
        }

        private void MoveUseFlowField()
        {
            Cell cellBlow = gridController.CurFlowField.GetCellFromWorldPos(transform.position);
            direction = new Vector3(cellBlow.BestDirection.Vector.x, 0, cellBlow.BestDirection.Vector.y);
            if (direction == Vector3.zero)
            {
                direction = (target.position - transform.position).normalized;
            }
            rigidBody.position += direction * owner.Data.MoveSpeed * Time.deltaTime;
        }

        float currentVelocity;
        float smoothTime = 0.2f;
        public void Rotate()
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        public void LookAtTarget(Transform target)
        {
            var dir = (target.position - transform.position).normalized;
            var targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }

        private void AttackEnd()
        {
            IsAttackEnd = true;
            Debug.Log("AttackEnd!");
        }

        private void HitCheck()
        {
            var hit = Physics.OverlapSphere(transform.position, owner.Data.AttackRange, LayerMask.GetMask("Player"));
            if(hit.Length > 0)
            {
                Debug.Log("HitCheck");
                var takedamageable = hit[0].GetComponent<ITakeDamageable>();
                takedamageable.TakeDamage(new DamageInfo(gameObject, owner.Data.AttackPower));
            }
        }
    }
}
