using Cysharp.Threading.Tasks;
using RPG.Combat;
using RPG.Combat.Skill;
using RPG.Core;
using RPG.Core.Data;
using RPG.Core.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace RPG.Control
{
    public class PlayerSkillController : MonoBehaviour
    {
        Player owner;
        public Dictionary<int, SkillBase> SkillDict;
        public List<SkillBase> SkillList;

        private void Awake()
        {
            SkillDict = new Dictionary<int, SkillBase>();
            SkillList = new List<SkillBase>();
            owner = GetComponent<Player>();
        }

        private void Start()
        {
            // 기본 공격
            AddSkill(owner.Data.DefaultSkillId, 1);
        }

        public void DestroySkill()
        {
            SkillList.ForEach(skill => Destroy(skill.gameObject));
        }

        public void AddSkill(int id, int level)
        {
            var skill = Managers.Instance.Skill.GetSkill<ActiveSkill>(owner, id, level);
            SkillDict.Add(id, skill);
            SkillList.Add(skill);
            skill.UseSkill().Forget();
        }


        /// <summary>
        /// 만약 스킬(id)을 가지고 있다면 레벨업을 하고 아니면 스킬을 추가해준다.
        /// </summary>
        /// <param name="id"></param>
        public void TryLevelup(int id)
        {
            if(SkillDict.TryGetValue(id, out SkillBase skill) == false)
            {
                // add
                AddSkill(id, 1);
            }
            else
            {
                Levelup(id);
            }
        }

        private void Levelup(int id)
        {
            var skill = SkillDict[id];
            Levelup(skill);
        }

        private void Levelup(SkillBase skill)
        {
            int nextLevel = skill.Data.Level + 1;
            skill.SetData(Managers.Instance.Skill.GetSkillData(skill.Data.Id, nextLevel));
        }

        public List<SkillData> GetRandomSkillList(int count)
        {
            var playerSkillIdList = Managers.Instance.Skill.SkillIdList;
            List<SkillData> randomSkillList = new List<SkillData>();

            var result = Util.Utility.GetShuffledList(playerSkillIdList);
            // 플레이어가 이미 가지고 있는 스킬이면 레벨을 그다음레벨로
            // 만약 맥스 레벨이면 
            // 플레이어가 가지고 있는 스킬 중 랜덤으로
            for (int i = 0; i < count; ++i)
            {
                int randomskillId = result[i];


                // 플레이어가 소지중인 스킬 일 때
                if (ContainsSkill(randomskillId, out SkillData skillData) == true)
                {
                    int nextLevel = skillData.Level + 1;
                    var nextLevelData = Managers.Instance.Skill.GetSkillData(randomskillId, nextLevel);

                   randomSkillList.Add(nextLevelData);
                }
                // 플레이어가 소지하고 있지 않은 스킬 일 때
                else
                {
                    randomSkillList.Add(Managers.Instance.Skill.GetSkillData(randomskillId, 1));
                }

            }

            return randomSkillList;
        }

        private bool ContainsSkill(int randomskillId, out SkillData data)
        {
            if (SkillDict.ContainsKey(randomskillId) == true)
            {
                data = SkillDict[randomskillId].Data;
                return true;
            }
            else
            {
                data = null;
            }

            return false;
        }
    }
}
