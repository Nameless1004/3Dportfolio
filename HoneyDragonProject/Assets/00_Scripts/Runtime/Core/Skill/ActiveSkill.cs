using UnityEngine;

namespace RPG.Core.Skill
{
    public abstract class ActiveSkill : SkillBase
    {
        private float remainingCoolTime;
        private float elapsedTime;

        public float RemainingCoolTime => remainingCoolTime;

        public abstract void Activate();
        
        public virtual bool CanUseSkill()
        {
            return elapsedTime > data.SkillCoolTime;
        }

        public void SkillCoolTimeUpdate()
        {
            elapsedTime += Time.deltaTime;
            remainingCoolTime = Mathf.Clamp(data.SkillCoolTime - elapsedTime, 0, data.SkillCoolTime);
        }

        public void ResetTimer()
        {
            elapsedTime = 0;
        }
    }
}
