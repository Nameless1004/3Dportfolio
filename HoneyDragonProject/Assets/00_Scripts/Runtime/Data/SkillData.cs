using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Core
{
    [Serializable]
    public class SkillData : Data
    {
        public string   IconPath;
        public string   Name;
        public string   Description;
        public float    SkillCoolTime;
        public int      Level;
        public int      MinDamage;
        public int      MaxDamage;

        public override void Load()
        {
        }

        public override void Save()
        {
        }
    }
}
