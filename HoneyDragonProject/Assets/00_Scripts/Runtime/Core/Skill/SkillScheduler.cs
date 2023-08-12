using RPG.Core.Skill;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Core.Skill
{
    public class SkillScheduler : MonoBehaviour
    {
        Dictionary<string, ISkill> skills = new Dictionary<string, ISkill>();

        private void Start()
        {
            skills.Add("BasicAttack", SkillManager.Instance.GetSkill("BasicAttack"));
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                skills.Values.ToList().ForEach(x => x.LevelUp());
            }

            foreach(var skill in skills.Values) 
            {
                if(skill is ActiveSkill)
                {
                    var active = skill as ActiveSkill;
                    if(active.CanUseSkill())
                    {
                        active.Activate();
                    }
                    active.SkillCoolTimer();
                }
            }
        }

        private void LevelUpSkill(string name)
        {
            skills[name].LevelUp();
        }
    }
}
