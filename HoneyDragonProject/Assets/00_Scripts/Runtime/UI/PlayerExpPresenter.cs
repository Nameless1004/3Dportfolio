using RPG.Core;
using RPG.Core.Manager;
using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.UI
{
    public class PlayerExpPresenter : BaseUI
    {
        Player model;
        PlayerExpView view;


        public void SetModel(Player model)
        {
            this.model = model;
            model.OnGetExp -= OnGetExp;
            model.OnGetExp += OnGetExp;
        }

        private void OnDestroy()
        {
            model.OnGetExp -= OnGetExp;
        }

        public void OnGetExp(float expRatio)
        {
            view.UpdateExpBar(expRatio);
        }

        public override void Init()
        {
            SetModel(Managers.Instance.Game.CurrentPlayer);
            view = GetComponentInChildren<PlayerExpView>();
            view.UpdateExpBar(0f);
        }
    }
}
