using RPG.Combat.Skill;
using RPG.Control;
using RPG.Core.Data;
using RPG.Core.Manager;
using System;
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
        List<LevelupSkillButton> levelupSkillButtons;
        [SerializeField] LevelupSkillButton levelupSkillButtonPrefab;
        HashSet<int> randomFilter;
        Player currentPlayer;
        PlayerSkillController currentPlayerSkillController;


        private void OnEnable()
        {
            Logger.Log("OnEnable");
            // 버튼 이벤트 등록
            CloseButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            currentPlayer.OnLevelup -= Show;
        }

        private void OnDisable()
        {
            Logger.Log("OnDisable");
            CloseButton.onClick.RemoveAllListeners();
        }

        public override void Show()
        {
            base.Show();

            // 랜덤 3개를 가져온다.
            var randomSkillList = currentPlayerSkillController.GetRandomSkillList(3);

            SetLevelupSkillButton(randomSkillList);
            Time.timeScale = 0f;
        }

        private void SetLevelupSkillButton(List<SkillData> randomSkillList)
        {
            for(int i = 0; i < 3; ++i)
            {
                levelupSkillButtons[i].BindSkillData(randomSkillList[i]);
            }
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1f;
        }


        public override void Init()
        {
            Logger.Log("Init");
            randomFilter = new HashSet<int>();
            currentPlayer = Managers.Instance.Game.CurrentPlayer;
            currentPlayerSkillController = currentPlayer.GetComponentInChildren<PlayerSkillController>();
            levelupSkillButtons = new List<LevelupSkillButton>();
            for (int i = 0; i < 3; ++i)
            {
                var lsb = Instantiate(levelupSkillButtonPrefab, contents);
                levelupSkillButtons.Add(lsb);
                levelupSkillButtons[i].OnButtonClicked += OnClickedSkillInfo;
            }
            currentPlayer.OnLevelup += Show;
            Hide();
            //currentPlayer.OnLevelup += Show;
        }

    

        public  void OnClickedSkillInfo(LevelupSkillButton selectedButton)
        {
            Debug.Log("Click!");
            // Test
            var con = currentPlayer.GetComponent<PlayerSkillController>();
            con.TryLevelup(selectedButton.SkillData.Id);
            Hide();
        }
    }
}
