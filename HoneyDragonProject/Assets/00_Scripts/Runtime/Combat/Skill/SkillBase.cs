using Cysharp.Threading.Tasks;
using RPG.Core;
using RPG.Core.Data;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public interface Skill
    {
        public SkillData Data { get;}
    }

    public abstract class SkillBase : MonoBehaviour, Skill
    {
        protected Creature owner;
        public int Id;
        public Sprite Icon;
        public int CurrentLevel;

        [field: SerializeField]
        public SkillData Data { get; protected set; }

        public void SetOwner(Creature owner) => this.owner = owner;
        public virtual void SetData(SkillData data)
        {
            Data = data;
            Id = data.Id;
        }
    }

    public abstract class ActiveSkill : SkillBase
    {
        protected string activeSoundResourcePath;
        public abstract UniTaskVoid UseSkill();
        protected abstract UniTaskVoid Activate(Creature initiator);
    }


}
