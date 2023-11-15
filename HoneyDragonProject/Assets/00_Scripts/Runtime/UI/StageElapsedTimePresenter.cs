using RPG.Core.Manager;
using RPG.Core.Scene;

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
