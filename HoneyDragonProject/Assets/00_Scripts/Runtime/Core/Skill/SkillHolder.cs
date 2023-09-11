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
            currentSkill = new BasicAttack();
            currentSkill.Data = Managers.Instance.Skill.GetSkillData(0000, 1);
            currentSkillData = currentSkill.Data;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Use();
            }
        }

        public void Holding(ActiveSkill skill)
        {
            this.currentSkill = skill;
            currentSkillData = currentSkill.Data;
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
