using RPG.Core.Creature;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RPG.Core.Skill
{
    public class SkillManager : MonoBehaviour
    {
        static public SkillManager Instance => instance;
        static private SkillManager instance;
        Dictionary<string, ISkill> skills = new Dictionary<string, ISkill>();


        private void Awake()
        {
            instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }

        private void Init()
        {
            AddAllSkillds();
        }

        private void AddAllSkillds()
        {
            AddSkill("BasicAttack");
        }

        private void AddSkill(string name)
        {
            var skill = Resources.Load<ActiveSkill>(name);
            Debug.Assert(skill != null, "Invalid skill name");

            skill.Owner = GameObject.FindWithTag("Player").GetComponent<Player>();
            skill.Init();
            skills.Add(name, skill);
        }

        public ISkill GetSkill(string name)
        {
            return skills[name];
        }

        public SkillData GetSkillData(string skillName, int level)
        {
            string json = File.ReadAllText(StaticPath.SkillDataPath + $"{skillName}_{level}.json");
            return JsonUtility.FromJson<SkillData>(json);
        }
    }
}
