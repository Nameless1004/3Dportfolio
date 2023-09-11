using System.Collections.Generic;
using System;
using RPG.Core;

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
public class SkillDataSet : ILoader<int, Dictionary<int, SkillData>>
{
    public List<SkillData> SkillDatas = new List<SkillData>();

    public Dictionary<int, Dictionary<int, SkillData>> MakeDict()
    {
        Dictionary<int, Dictionary<int, SkillData>> dic = new Dictionary<int, Dictionary<int, SkillData>>();

        SkillDatas.ForEach(x =>
        {
            if(dic.ContainsKey(x.Id) == true)
            {
                dic[x.Id].Add(x.Level, x);
            }
            else
            {
                dic.Add(x.Id, new Dictionary<int, SkillData>());
                dic[x.Id].Add(x.Level, x);
            }
        }
        );
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