using RPG.Combat;
using RPG.Control;
using RPG.Core.Manager;
using RPG.Core.Scene;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RPG.Core.UI
{
    public class ResultUI : Popup
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private TMP_Text TimeInfo;
        [SerializeField] private TMP_Text KillInfo;
        [SerializeField] private Transform SkillListInfo;
        private CanvasGroup group;
        public override CanvasGroup CanvasGroup => group;


        private void OnFadeInDone()
        {
            group.interactable = true;
        }

        private void OnFadeOutDone()
        {
            SceneManager.LoadScene((int)SceneType.Lobby);
        }
        
        public override void Close()
        {
            group = group == null ? GetComponent<CanvasGroup>() : group;

            closeButton.onClick.RemoveListener(Close);
            OnFadeinDone -= OnFadeInDone;
            OnFadeoutDone -= OnFadeOutDone;

            group.interactable = false;
            SimpleFadeOut(FadeOutTime).Forget();
        }

        public override void Open()
        {
            group = group == null ? GetComponent<CanvasGroup>() : group;

            closeButton.onClick.AddListener(Close);

            OnFadeinDone -= OnFadeInDone;
            OnFadeoutDone -= OnFadeOutDone;
            OnFadeinDone += OnFadeInDone;
            OnFadeoutDone += OnFadeOutDone;

            group.alpha = 0f;
            group.interactable = false;
            SetUIData();
            SimpleFadeIn(FadeInTime).Forget();
        }

        private void SetUIData()
        {
            TimeSpan span = TimeSpan.FromSeconds(Managers.Instance.Game.GameScene.GameElapsedTime);
            TimeInfo.text = $"{span.Minutes:D2} : {span.Seconds:D2}";

            KillInfo.text = Managers.Instance.Game.GameScene.MonsterSpawner.KillCount.ToString() + " Kills";

            //SkillListInfo √ ±‚»≠
            SkillListInfo.DestroyChildren();
            var currentPlayer = Managers.Instance.Game.CurrentPlayer;
            var skillList = currentPlayer.GetComponent<PlayerSkillController>().SkillList;
            var skillIconPrefab = Resources.Load<Image>("Prefab/UI/SkillIcon");
            skillList.ForEach(skill =>
            {
                Instantiate<Image>(skillIconPrefab, SkillListInfo).sprite = Resources.Load<Sprite>(skill.Data.IconPath);
            });
        }
    }
}
