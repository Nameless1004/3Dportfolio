using RPG.Core.Manager;

namespace RPG.Core.UI
{
    public class PlayerExpPresenter : BaseUI
    {
        Player model;
        PlayerExpView view;

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
            model = Managers.Instance.Game.CurrentPlayer;
            model.OnGetExp -= OnGetExp;
            model.OnGetExp += OnGetExp;

            view = GetComponentInChildren<PlayerExpView>();
            view.UpdateExpBar(0f);
        }
    }
}
