using Cysharp.Threading.Tasks;
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

        private HashSet<int> randomFilter;

        private void Awake()
        {
            randomFilter = new HashSet<int>();
            SkillDict = new Dictionary<int, SkillBase>();
            SkillList = new List<SkillBase>();
            owner = GetComponent<Player>();
        }

        private void Start()
        {
            // 기본 공격
            AddSkill(2004, 1);
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

            // 플레이어가 이미 가지고 있는 스킬이면 레벨을 그다음레벨로
            // 만약 맥스 레벨이면 
            // 플레이어가 가지고 있는 스킬 중 랜덤으로
            for (int i = 0; i < count; ++i)
            {
                int randomskillId = playerSkillIdList.GetRandomValue<int>();

                // TODO: 플레이어 스킬 추가하면 삭제 예정
                // 플레이어 스킬이 3개 미만일 때에는 중복허용 랜덤
                if (playerSkillIdList.Count >= 3)
                {
                    while (randomFilter.Contains(randomskillId) == true)
                    {
                        randomskillId = playerSkillIdList.GetRandomValue<int>();
                    }
                }

                randomFilter.Add(randomskillId);

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
            randomFilter.Clear();

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
