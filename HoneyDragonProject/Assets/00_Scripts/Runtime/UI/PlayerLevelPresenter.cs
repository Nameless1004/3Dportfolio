using RPG.Core.Manager;
using RPG.Core.Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.UI
{
    public class PlayerLevelPresenter : BaseScene
    {
        public override void Clear()
        {
            model.OnLevelup -= OnLevelUpdated;
        }

        public override void Init()
        {
            model = Managers.Instance.Game.CurrentPlayer;
            view = GetComponentInChildren<PlayerLevelView>();
            model.OnLevelup += OnLevelUpdated;

            // 초기 레벨 설정 : 1
            OnLevelUpdated(model.Status.Level);
        }

        Player model;
        PlayerLevelView view;

        void OnLevelUpdated(int level)
        {
            view.OnLevelUpdated(level);
        }
    }
}
