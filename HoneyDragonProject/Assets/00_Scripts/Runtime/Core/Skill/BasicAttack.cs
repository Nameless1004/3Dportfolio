using UnityEngine;

namespace RPG.Core.Skill
{
    public class BasicAttack : ActiveSkill
    {
        public override void Activate()
        {
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
