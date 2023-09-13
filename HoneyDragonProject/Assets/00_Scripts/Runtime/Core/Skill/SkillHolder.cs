using RPG.Core.Data;
using RPG.Core.Manager;
using System.Collections;
using UnityEngine;

namespace RPG.Core.Skill
{
    public class SkillHolder : MonoBehaviour
    {
        private ActiveSkill currentSkill;
        private SkillData currentSkillData;

        private void Start()
        {
            Holding(new BasicAttack());
        }

        public void Holding(ActiveSkill skill)
        {
            this.currentSkill = skill;

            // Temp
            currentSkill.Data = Managers.Instance.Skill.GetSkillData(0000, 1);

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
                    currentSkill.Activate();
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
