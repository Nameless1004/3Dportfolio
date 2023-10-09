using RPG.Control;
using RPG.Core.Data;
using RPG.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Core.UI
{
    public class LevelupUI : BaseUI
    {
        
        [SerializeField] Button CloseButton;
        [SerializeField] RectTransform UI;
        [SerializeField] RectTransform contents;
        List<LevelupSkillButton> contentButtons;
        Player currentPlayer;


        private void OnEnable()
        {
            Logger.Log("OnEnable");
            // 버튼 이벤트 등록
            contentButtons.ForEach(button => button.OnButtonClicked += OnClickedSkillInfo);
            CloseButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            currentPlayer.OnLevelup -= Show;
        }

        private void OnDisable()
        {
            Logger.Log("OnDisable");
            contentButtons.ForEach(button => button.OnButtonClicked -= OnClickedSkillInfo);
            CloseButton.onClick.RemoveAllListeners();
        }

        public override void Show()
        {
            base.Show();
            var playerSkills = currentPlayer.GetComponent<PlayerSkillController>().SkillList;
            Time.timeScale = 0f;
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1f;
        }


        public override void Init()
        {
            Logger.Log("Init");
            currentPlayer = Managers.Instance.Game.CurrentPlayer;
            contentButtons = contents.GetComponentsInChildren<LevelupSkillButton>().ToList();
            contentButtons[0].BindSkillData(Managers.Instance.Skill.GetSkillData(2000, 1));
            currentPlayer.OnLevelup += Show;
            Hide();
            //currentPlayer.OnLevelup += Show;
        }

        public  void OnClickedSkillInfo(LevelupSkillButton selectedButton)
        {
            Debug.Log("Click!");
            // Test
            var con = currentPlayer.GetComponent<PlayerSkillController>();
            con.Levelup(selectedButton.SkillData.Id);
            Hide();
        }
    }
}
