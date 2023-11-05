using RPG.Core.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Core.UI
{
    public class LevelupSkillButton : MonoBehaviour
    {
        public Image SkillIcon;
        public TMP_Text SkillLevelupDescription;
        public SkillData SkillData;
        private Button button;
        public event Action<LevelupSkillButton> OnButtonClicked;


        public void BindSkillData(SkillData skillData)
        {
            if (skillData == null)
            {
                SkillIcon.sprite = null;
                SkillLevelupDescription.text = "강화할 수 있는 스킬이 없습니다.";
                return;
            }
            SkillData = skillData;
            SkillIcon.sprite = Resources.Load<Sprite>(skillData.IconPath);
            string description = $"이름: {skillData.Name}\n{skillData.Description}";
            SkillLevelupDescription.text = description;
        }

        private void Awake()
        {
            button = GetComponentInChildren<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        private void OnButtonClick()
        {
            if (SkillData == null) return;
            OnButtonClicked?.Invoke(this);
        }

    }
}
