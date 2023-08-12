using UnityEngine;

namespace RPG.Core.Skill
{
    public abstract class ActiveSkill : SkillBase
    {
        protected float elapsedTime;


        public abstract void Activate();
        
        public virtual bool CanUseSkill()
        {
            return elapsedTime > data.SkillCoolTime;
        }

        public void SkillCoolTimer()
        {
            elapsedTime += Time.deltaTime;
        }

        public void ResetTimer()
        {
            elapsedTime = 0;
        }
    }
}
