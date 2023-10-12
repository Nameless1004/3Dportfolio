using RPG.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.UI
{
    public class StageElapsedTimePresenter : BaseUI
    {
        GameScene model;
        StageElapsedTimeView view;
        public override void Init()
        {
            model = Managers.Instance.Game.GameScene;
            view = GetComponentInChildren<StageElapsedTimeView>();
            model.OnTimeChanged += UpdateElapsedTime;
        }

        public void UpdateElapsedTime(float elapsedTime)
        {
            view.UpdateElapsedTime(elapsedTime);
        }
    }
}
