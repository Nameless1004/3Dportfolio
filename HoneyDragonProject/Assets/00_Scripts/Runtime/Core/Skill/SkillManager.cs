using RPG.Core;
using System.Collections.Generic;
using System.Diagnostics;

public class SkillManager
{
    
    public Dictionary<int, Dictionary<int, SkillData>> skillData;


    public SkillData GetSkillData(int id, int level)
    {
        if(DataValidationCheck(id, level) == false)
        {
            Debug.Assert(true, "InValid SkillData");
            return null;
        }

        return skillData[id][level];
    }

    // ��ų ����
    public void Init(DataManager data)
    {
        skillData = data.SkillDataDict;
        Debug.Assert(skillData is not null);
        // GetSkillPrefabs
        // SkillDataSetting
    }

    public void LevelupSkill(int id, int currentLevel)
    {
        var data = GetSkillData(id, currentLevel + 1);
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

}
