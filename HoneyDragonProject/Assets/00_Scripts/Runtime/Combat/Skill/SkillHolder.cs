using Cysharp.Threading.Tasks;
using RPG.Core;
using RPG.Core.Data;
using RPG.Core.Manager;
using System.Collections;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public class SkillHolder : MonoBehaviour
    {
        [SerializeField] Creature owner;
        [Tooltip("Test용 입니다: 스킬 id 입력")]
        public int skillId;

        private ActiveSkill currentSkill;
        private SkillData currentSkillData;

        private void Start()
        {
            Holding(Managers.Instance.Skill.GetSkill<ActiveSkill>(skillId));
        }

        public void Holding(ActiveSkill skill)
        {
            currentSkill = skill;

            // Temp
            currentSkillData = currentSkill.Data;
            Use();
        }

        public void LevelUp()
        {
            currentSkill.Levelup();
            Debug.Log("LevelUp!!");
        }


        public void Use()
        {
            SkillUse().Forget();
        }

        private async UniTaskVoid SkillUse()
        {
            while(true)
            {
                var cancelToken = this.GetCancellationTokenOnDestroy();
                if (currentSkill.currentCoolTime > currentSkillData.CoolTime) 
                {
                    currentSkill.Activate(owner, cancelToken).Forget();
                    currentSkill.currentCoolTime = 0f;
                }
                else
                {
                    currentSkill.currentCoolTime += Time.deltaTime;
                }

                await UniTask.Yield(cancelToken);
            }
        }
    }
}
