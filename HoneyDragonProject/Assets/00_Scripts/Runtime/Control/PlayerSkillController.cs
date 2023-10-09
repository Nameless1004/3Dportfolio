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
        public List<Skill> SkillList;

        private void Awake()
        {
            SkillList = new List<Skill>();
            owner = GetComponent<Player>();
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
            SkillList.Add(skill);
            skill.UseSkill().Forget();
        }

        public void Levelup(int id)
        {
            var skill = SkillList.Find(x => x.Data.Id == id);
            Levelup(skill);
        }
        public void Levelup(Skill skill)
        {
            int nextLevel = skill.Data.Level + 1;
            ((SkillBase)skill).SetData(Managers.Instance.Skill.GetSkillData(skill.Data.Id, nextLevel));
        }
    }
}
