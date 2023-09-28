using Cysharp.Threading.Tasks;
using RPG.Combat.Skill;
using RPG.Core;
using RPG.Core.Manager;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace RPG.Control
{
    public class PlayerSkillController : MonoBehaviour
    {
        Player owner;
        List<Skill> skillList;

        private void Awake()
        {
            owner = GetComponent<Player>();
            skillList = new List<Skill>();
        }

        private void Start()
        {
            // 기본 공격
            AddSkill(2000, 1);
        }

        //public async UniTaskVoid UseSkill()
        //{
        //    while(true)
        //    {
        //        await UniTask.Yield(this.GetCancellationTokenOnDestroy());

        //        foreach(var skill in skillList)
        //        {
        //            if(skill is ActiveSkill)
        //            {
        //                var active = skill as ActiveSkill;
        //                active.UseSkill();
        //            }
        //        }
        //    }
        //}

        public void AddSkill(int id, int level)
        {
            var skill = Managers.Instance.Skill.GetSkill<ActiveSkill>(owner, id, level);
            skillList.Add(skill);
            skill.UseSkill().Forget();
        }

        public void Levelup(Skill skill)
        {
            int nextLevel = skill.Data.Level + 1;
            int index = skillList.FindIndex(0, x => x == skill);
            if(index < 0)
            {
                // error
            }
            (skillList[index] as SkillBase).SetData(Managers.Instance.Skill.GetSkillData(skill.Data.Id, nextLevel));
        }
    }
}
