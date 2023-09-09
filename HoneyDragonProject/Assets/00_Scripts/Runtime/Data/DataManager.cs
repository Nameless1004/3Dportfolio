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
    public Dictionary<int, SkillData> SkillDataDict { get; private set; }
    public void Init()
    {
        SkillDataDict = LoadJson<SkillDataSet, int, SkillData>("Skill/BasicAttack").MakeDict();
    }
    
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value> 
    {
        var textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
