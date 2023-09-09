using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Dictionary<int, SkillData>> SkillDataDict { get; private set; }
    public Dictionary<int, EnemyData> EnemyDataDict { get; private set; }

    public void Init()
    {
        SkillDataDict = LoadJson<SkillDataSet, int, Dictionary<int, SkillData>>("Skill/BasicAttack").MakeDict();
        EnemyDataDict = LoadJson<EnemyDataSet, int, EnemyData>("Enemy/Enemies").MakeDict();
    }
    
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value> 
    {
        var textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
