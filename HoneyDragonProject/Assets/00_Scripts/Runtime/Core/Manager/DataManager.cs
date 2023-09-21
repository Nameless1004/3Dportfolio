using RPG.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Core.Manager
{
    public class DataManager : IManager
    {
        public Dictionary<int, Dictionary<int, SkillData>> SkillDataDict { get; private set; }
        public Dictionary<int, EnemyData> EnemyDataDict { get; private set; }
        public Dictionary<int, StageData> StageDataDict { get; private set; }

        public void Init()
        {
            SkillDataDict = LoadJsonAll<SkillDataSet, int, Dictionary<int, SkillData>>("Skill/");
            EnemyDataDict = LoadJson<EnemyDataSet, int, EnemyData>("Enemy/Enemies").MakeDict();
            StageDataDict = LoadJson<StageDataSet, int, StageData>("Stage/Stage").MakeDict();
        }


        // 스킬 데이터 -> 스킬 id, (스킬 레벨, 스킬 데이터)

        Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
            var textAsset = Resources.Load<TextAsset>($"Data/{path}");
            return JsonUtility.FromJson<Loader>(textAsset.text);
        }

        Dictionary<Key, Value> LoadJsonAll<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
            var textAsset = Resources.LoadAll<TextAsset>($"Data/{path}");
            List<Dictionary<Key, Value>> dicts = new List<Dictionary<Key, Value>>();

            foreach(var i in textAsset)
            {
                dicts.Add(JsonUtility.FromJson<Loader>(i.text).MakeDict());
            }

            var mergedDictionary = dicts.Aggregate((dict1, dict2) => dict1.Concat(dict2).ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
            return mergedDictionary;
        }
    }
}
