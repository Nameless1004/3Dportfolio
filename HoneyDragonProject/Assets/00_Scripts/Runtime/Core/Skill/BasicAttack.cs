using UnityEngine;

namespace RPG.Core.Skill
{
    public class BasicAttack : ActiveSkill
    {
        [SerializeField] GameObject Bullet;

        public override void Activate()
        {
            var player = GameObject.FindWithTag("Player");
            var bullet = Instantiate(Bullet).GetComponent<ProjectileTest>();
            bullet.SetProjectile(player.transform.position, player.transform.forward, 10f, 2f, 1000);
            Debug.Log("기본 공격");
            Debug.Log($"damage : {data.MaxDamage}");
            ResetTimer();
        }

        public override void Init()
        {
            data = SkillManager.Instance.GetSkillData("BasicAttack", 1);
            ExtractSkillIcon();
        }

        public override void LevelUp()
        {
            data = SkillManager.Instance.GetSkillData("BasicAttack", ++data.Level);
        }
    }
}
