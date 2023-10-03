using RPG.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Core.UI
{
    public class LevelupUI : BaseUI
    {
        
        [SerializeField] Button CloseButton;
        [SerializeField] RectTransform UI;
        Player currentPlayer;


        private void OnEnable()
        {
            Debug.Log("OnEnable");
            // 버튼 이벤트 등록
            CloseButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            currentPlayer.OnLevelup -= Show;
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
            CloseButton.onClick.RemoveAllListeners();
        }

        public override void Show()
        {
            base.Show();
            Time.timeScale = 0f;
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1f;
        }


        public override void Init()
        {
            Debug.Log("Init");
            currentPlayer = Managers.Instance.Game.CurrentPlayer;
            currentPlayer.OnLevelup += Show;
            Hide();
            //currentPlayer.OnLevelup += Show;
        }
    }
}
