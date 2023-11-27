using RPG.Combat;
using RPG.Core.Manager;
using RPG.Core.UI;
using System.Linq;
using UnityEngine;

namespace RPG.Core.Scene
{
    public class UIScene : BaseScene
    {
        public Canvas MainCanvas;
        public override void Init()
        {
            // 현재 씬에 있는 BaseUI를 상속받은 모든 객체의 초기화를 UI Scene에서 해줌
            MainCanvas.GetComponentsInChildren<BaseUI>().ToList().ForEach(x => {
                x.Init();
                Logger.Log(x.gameObject.name);
            });

            Managers.Instance.Game.CurrentPlayer.GetComponent<Health>().OnDie += OnPlayerDie;
            Managers.Instance.Game.GameScene.OnGameClear += OnGameClear;
        }


        public override void Clear()
        {
        }

        private void OnPlayerDie() 
        {
            var resultUI = Instantiate(Util.ResourceCache.Load<ResultUI>("Prefab/UI/ResultUI"), MainCanvas.transform);
            resultUI.transform.SetAsLastSibling();
            resultUI.Open();
        }

        private void OnGameClear()
        {
            var resultUI = Instantiate(Util.ResourceCache.Load<ResultUI>("Prefab/UI/ResultUI"), MainCanvas.transform);
            resultUI.transform.SetAsLastSibling();
            resultUI.FadeInTime = 5f;
            resultUI.Open();
        }
    }
}
