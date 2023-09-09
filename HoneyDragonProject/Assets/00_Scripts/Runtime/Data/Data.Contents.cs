using System.Collections.Generic;
using System;

#region SkillStat
[Serializable]
public class SkillData
{
    public int Id;
    public string IconPath;
    public int Level;
    public string Name;
    public string Description;
    public int MinDamage;
    public int MaxDamage;
    public float CoolTime;
}

[Serializable]
public class SkillDataSet : ILoader<int, SkillData>
{
    public List<SkillData> SkillDatas = new List<SkillData>();

    public Dictionary<int, SkillData> MakeDict()
    {
        Dictionary<int, SkillData> dic = new Dictionary<int, SkillData>();
        SkillDatas.ForEach(x => dic.Add(x.Id, x));
        return dic;
    }
}
#endregion

#region Enemy
public class EnemyData
{
    public int Id;
    public string Name;
    public int Hp;
    public int AttackPower;
    public int AttackRange;
    public float AttackRate;
}

public class EnemyDataSet : ILoader<int, EnemyData>
{
    public List<EnemyData> EnemyDatas = new List<EnemyData>();
    public Dictionary<int, EnemyData> MakeDict()
    {
        var dict = new Dictionary<int, EnemyData>();
        EnemyDatas.ForEach(x => dict.Add(x.Id, x));
        return dict;
    }
}
#endregion