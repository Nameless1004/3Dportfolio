using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Core.Skill
{
    [Serializable]
    public class SkillData
    {
        public string   IconPath;
        public string   Name;
        public string   Description;
        public float    SkillCoolTime;
        public int      Level;
        public int      MinDamage;
        public int      MaxDamage;
    }
}
