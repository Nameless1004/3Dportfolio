using RPG.Combat.Skill;
using RPG.Core.Data;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Manager
{
    public class SkillManager : IManager
    {
        private Dictionary<int, Dictionary<int, SkillData>> skillData;
        public Dictionary<int, Skill> SkillDict;


        public T GetSkill<T>(int id) where T : Skill
        {
            return (T)SkillDict[id];
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

        private void InitSkill()
        {
            SkillBase skill = null;

            skill = new FireBall(GetSkillData(1001, 1));
            SkillDict.Add(skill.Id, skill);
        }

        public SkillData LevelupSkill(int id, int currentLevel)
        {
            if (DataValidationCheck(id, currentLevel + 1) == false)
            {
                // Debug.Log(true, $"SkillData Invalid ; Skill ID : {id}, Request Level : {currentLevel + 1}");
                return null;
            }

            return GetSkillData(id, currentLevel + 1); ;
        }

        // true : ��ȿ, false = ��ȿ���� ����
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
            SkillDict = new Dictionary<int, Skill>();
            skillData = dataManager.SkillDataDict;
            Debug.Assert(skillData is not null);
            InitSkill();
            // GetSkillPrefabs
            // SkillDataSetting
        }
    }
}
