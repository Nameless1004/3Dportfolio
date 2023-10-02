using RPG.Combat.Skill;
using RPG.Core.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Manager
{
    public class SkillManager : IManager
    {
        private Dictionary<int, Dictionary<int, SkillData>> skillData;

        public T GetSkill<T>(Creature owner, int id, int level, Transform parent = null) where T : SkillBase
        {
            var skillData = GetSkillData(id, level);
            SkillBase skill = MonoBehaviour.Instantiate(Resources.Load<SkillBase>(skillData.PrefabPath), parent);
            skill.SetData(skillData);
            skill.SetOwner(owner);
            return (T)skill;
        }

        public SkillData GetSkillData(int id, int level)
        {
            if (DataValidationCheck(id, level) == false)
            {
                Debug.Assert(true, "InValid SkillData");
                return null;
            }

            return skillData[id][level];
        }

        public SkillData LevelupSkill(int id, int currentLevel)
        {
            if (DataValidationCheck(id, currentLevel + 1) == false)
            {
                Debug.Assert(true, $"SkillData Invalid ; Skill ID : {id}, Request Level : {currentLevel + 1}");
                return null;
            }

            return GetSkillData(id, currentLevel + 1); ;
        }

        // true : 유효, false = 유효하지 않음
        private bool DataValidationCheck(int id, int level)
        {
            if (skillData.ContainsKey(id) && skillData[id].ContainsKey(level))
            {
                return true;
            }

            return false;
        }

        public void Init()
        {
            DataManager dataManager = Managers.Instance.Data;
            skillData = dataManager.SkillDataDict;
            Debug.Assert(skillData != null);
        }

        public void Clear()
        {

        }
    }
}
