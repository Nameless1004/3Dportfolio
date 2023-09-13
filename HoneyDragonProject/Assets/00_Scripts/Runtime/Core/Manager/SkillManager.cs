using RPG.Core;
using RPG.Core.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace RPG.Core.Manager
{
    public class SkillManager
    {

        public Dictionary<int, Dictionary<int, SkillData>> skillData;


        public SkillData GetSkillData(int id, int level)
        {
            if (DataValidationCheck(id, level) == false)
            {
                Debug.Assert(true, "InValid SkillData");
                return null;
            }

            return skillData[id][level];
        }

        // 스킬 세팅
        public void Init(DataManager data)
        {
            skillData = data.SkillDataDict;
            Debug.Assert(skillData is not null);
            // GetSkillPrefabs
            // SkillDataSetting
        }

        public SkillData LevelupSkill(int id, int currentLevel)
        {
            if (DataValidationCheck(id, currentLevel + 1) == false)
            {
                Debug.Assert(true, $"SkillData Invalid ; Skill ID : {id}, Request Level : {currentLevel + 1}");
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
    }
}
