using RPG.Core.Data;
using RPG.Util;
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
        public Dictionary<int, EnemyData> BossDataDict { get; private set; }
        public Dictionary<int, StageData> StageDataDict { get; private set; }

        public Dictionary<int, PlayerData> PlayerDataDict { get; private set; }
        public Dictionary<int, PlayerExpData> PlayerExpDataDict { get; private set; }

        public void Init()
        {
            SkillDataDict = LoadJsonAll<SkillDataSet, int, Dictionary<int, SkillData>>("Skill/");
            EnemyDataDict = LoadJson<EnemyDataSet, int, EnemyData>("Enemy/Enemies").MakeDict();
            BossDataDict = LoadJson<EnemyDataSet, int, EnemyData>("Enemy/Boss").MakeDict();
            StageDataDict = LoadJson<StageDataSet, int, StageData>("Stage/Stages").MakeDict();
            PlayerExpDataDict = LoadJson<PlayerExpDataSet, int, PlayerExpData>("Player/Exp").MakeDict();
            PlayerDataDict = LoadJson<PlayerDataSet, int, PlayerData>("Player/Player").MakeDict();
        }


        private T SimpleLoadJson<T>(string path)
        {
            // TODO: 
            // var textAsset = Resources.Load<TextAsset>($"Data/{path}");
            var textAsset = Resources.Load<TextAsset>($"EncryptedData/{path}");
            return JsonUtility.FromJson<T>(JsonUtil.Decrypt("test", textAsset.text));
        }

        // 스킬 데이터 -> 스킬 id, (스킬 레벨, 스킬 데이터)
        Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
            // TODO: 
            // var textAsset = Resources.Load<TextAsset>($"Data/{path}");
            var textAsset = Resources.Load<TextAsset>($"EncryptedData/{path}");
            return JsonUtility.FromJson<Loader>(JsonUtil.Decrypt("test", textAsset.text));
        }

        Dictionary<Key, Value> LoadJsonAll<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
            // TODO: 
            // var textAsset = Resources.LoadAll<TextAsset>($"Data/{path}");
            var textAsset = Resources.LoadAll<TextAsset>($"EncryptedData/{path}");
            List<Dictionary<Key, Value>> dicts = new List<Dictionary<Key, Value>>();

            foreach (var i in textAsset)
            {
                dicts.Add(JsonUtility.FromJson<Loader>(JsonUtil.Decrypt("test", i.text)).MakeDict());
            }

            var mergedDictionary = dicts.Aggregate((dict1, dict2) => dict1.Concat(dict2).ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
            return mergedDictionary;
        }

        public void Clear()
        {

        }
    }
}
