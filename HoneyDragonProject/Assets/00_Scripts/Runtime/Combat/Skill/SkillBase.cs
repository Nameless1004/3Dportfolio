using RPG.Core.Data;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public interface Skill
    {

    }

    public abstract class SkillBase : Skill
    {
        public Sprite Icon;
        public SkillData Data { get; set; }
    }

    public abstract class ActiveSkill : SkillBase
    {
        public abstract void Activate(Transform initiator);
        public float currentCoolTime = float.MaxValue;
    }


}
