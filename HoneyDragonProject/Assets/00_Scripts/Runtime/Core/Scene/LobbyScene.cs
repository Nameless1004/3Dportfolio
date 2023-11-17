using RPG.Core.Manager;
using RPG.Core.UI;
using UnityEngine.SceneManagement;

namespace RPG.Core.Scene
{
    public class LobbyScene : BaseScene
    {
        private StartGameUI startGameUi;
        public override void Init()
        {
            startGameUi = FindFirstObjectByType<StartGameUI>();
        }

        public override void Clear()
        {
        }

        public void OnClick_PlayButton()
        {
            Managers.Instance.Game.selectedCharacterId = startGameUi.SelectedCharacterId;
            SceneManager.LoadScene((int)SceneType.Game);
        }
    }
}
