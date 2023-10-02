using RPG.Core;
using RPG.Core.Manager;
using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.UI
{
    public class PlayerExpPresenter : MonoBehaviour
    {
        Player model;
        PlayerExpView view;


        public void SetModel(Player model)
        {
            this.model = model;
            model.OnGetExp += OnGetExp;
        }

        void Start()
        {
            view = GetComponentInChildren<PlayerExpView>();
            view.UpdateExpBar(0f);
        }

        private void OnDestroy()
        {
            model.OnGetExp -= OnGetExp;
        }

        public void OnGetExp(float expRatio)
        {
            view.UpdateExpBar(expRatio);
        }

    }
}
