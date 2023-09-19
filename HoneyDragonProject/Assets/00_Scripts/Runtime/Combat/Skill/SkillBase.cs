using RPG.Core;
using RPG.Core.Data;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public interface Skill
    {
        public SkillData Data { get;}
    }

    public abstract class SkillBase : Skill
    {
        public SkillBase(SkillData data)
        {
            Data = data;
            Id = data.Id;
        }
        public int Id;
        public Sprite Icon;
        public int CurrentLevel;

        public SkillData Data { get; protected set; }
        

        public abstract void Levelup();
    }

    public abstract class ActiveSkill : SkillBase
    {
        public ActiveSkill(SkillData data) : base(data)
        {
        }
        public abstract void Activate(Creature initiator);
        public float currentCoolTime = float.MaxValue;
    }


}
