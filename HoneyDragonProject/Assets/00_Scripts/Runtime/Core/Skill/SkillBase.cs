using UnityEngine;
using UnityEngine.UI;

namespace RPG.Core.Skill
{
    public abstract class SkillBase : MonoBehaviour, ISkill
    {
        public SkillData Data => data;

        protected Image icon;
        protected SkillData data;

        public abstract void Init();
        public abstract void LevelUp();

        public void ExtractSkillIcon()
        {
            icon = Resources.Load<Image>(data.IconPath);

            //Debug.Assert(icon is not null, $"Path invalid {data.IconPath}");
        }
    }
}
