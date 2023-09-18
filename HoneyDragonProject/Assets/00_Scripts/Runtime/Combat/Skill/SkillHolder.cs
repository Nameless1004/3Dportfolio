using RPG.Core.Data;
using RPG.Core.Manager;
using System.Collections;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class SkillHolder : MonoBehaviour
    {
        [SerializeField] Transform owner;
        private ActiveSkill currentSkill;
        private SkillData currentSkillData;

        private void Start()
        {
            Holding(Managers.Instance.Skill.GetSkill<ActiveSkill>(1001));
        }

        public void Holding(ActiveSkill skill)
        {
            currentSkill = skill;

            // Temp
            currentSkillData = currentSkill.Data;
            Use();
        }

        public void Use()
        {
            StartCoroutine(SkillUse());
        }

        private IEnumerator SkillUse()
        {
            while(true)
            {
                if(currentSkill.currentCoolTime > currentSkillData.CoolTime) 
                {
                    currentSkill.Activate(owner);
                    currentSkill.currentCoolTime = 0f;
                }
                else
                {
                    currentSkill.currentCoolTime += Time.deltaTime;
                }

                yield return null;
            }
        }
    }
}
