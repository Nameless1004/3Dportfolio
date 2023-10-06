﻿using System.Collections.Generic;
using System;
using RPG.Core;
using RPG.Core.Manager;

namespace RPG.Core.Data
{

    #region PlayerStat
    [Serializable]
    public class PlayerExpData
    {
        public int Level;
        public int NeedExp;
    }

    [Serializable]
    public class PlayerExpDataSet : ILoader<int, PlayerExpData>
    {
        public List<PlayerExpData> PlayerExpData = new List<PlayerExpData>();
        public Dictionary<int, PlayerExpData> MakeDict()
        {
            Dictionary<int, PlayerExpData> dict = new Dictionary<int, PlayerExpData>();
            PlayerExpData.ForEach(x => dict.Add(x.Level, x));
            return dict;
        }
    }

    [Serializable]
    public class PlayerData
    {
        public string PrefabPath;
        public int Level;
        public int Hp;
        public int Exp;
        public float MoveSpeed;
        public float AttackPower;
        public float Defense;
    }


    #endregion
    #region SkillStat
    [Serializable]
    public class SkillData
    {
        public int Id;
        public string IconPath;
        public string PrefabPath;
        public int Level;
        public string Name;
        public string Description;
        public int MinDamage;
        public int MaxDamage;
        public float CoolTime;
        public int SpawnCount;
        public float ProjectileSpeed;
        public int SpawnRateMilliSecond;
        public int SpawnLifeTimeMilliSecond;
    }

    [Serializable]
    public class SkillDataSet : ILoader<int, Dictionary<int, SkillData>>
    {
        public List<SkillData> SkillData = new List<SkillData>();

        public Dictionary<int, Dictionary<int, SkillData>> MakeDict()
        {
            Dictionary<int, Dictionary<int, SkillData>> dic = new Dictionary<int, Dictionary<int, SkillData>>();

            SkillData.ForEach(x =>
            {
                if (dic.ContainsKey(x.Id) == true)
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
    [Serializable]
    public class EnemyData
    {
        public int Id;
        public string PrefabPath;
        public string Name;
        public int Hp;
        public int AttackPower;
        public int AttackRange;
        public float AttackRate;
    }
    [Serializable]
    public class EnemyDataSet : ILoader<int, EnemyData>
    {
        public List<EnemyData> EnemyData = new List<EnemyData>();
        public Dictionary<int, EnemyData> MakeDict()
        {
            var dict = new Dictionary<int, EnemyData>();
            EnemyData.ForEach(x => dict.Add(x.Id, x));
            return dict;
        }
    }
    #endregion

    #region Stage
    [Serializable]
    public struct SpawnEnemyInfo
    {
        // type 0 : normal, 1 : boss
        public int Type;
        // 0 : Outside, 1: voids
        public int SpawnType;
        public int SpawnCount;
        public int Id;
    }

    [Serializable]
    public class StageData
    {
        public int StageNum;
        public int MaxSpawnCount;
        public float SpawnRate;
        public List<SpawnEnemyInfo> EnemyInfoList;
    }

    [Serializable]
    public class StageDataSet : ILoader<int, StageData>
    {
        public List<StageData> StageData = new List<StageData>();

        public Dictionary<int, StageData> MakeDict()
        {
            Dictionary<int, StageData> dict = new Dictionary<int, StageData>();
            StageData.ForEach(x => dict.Add(x.StageNum, x));
            return dict;
        }
    }
    #endregion
}