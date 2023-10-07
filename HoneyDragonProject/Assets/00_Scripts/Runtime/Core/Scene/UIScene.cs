using RPG.Core.UI;
using System.Linq;
using UnityEngine;

namespace RPG.Core.Scene
{
    public class UIScene : BaseScene
    {
        LevelupUI levelupUI;
        PlayerExpPresenter PlayerExpPresenter;

        public override void Init()
        {
            // 현재 씬에 있는 BaseUI를 상속받은 모든 객체의 초기화를 UI Scene에서 해줌
            FindObjectsOfType<BaseUI>().ToList().ForEach(x => {
                x.Init();
                Logger.Log(x.gameObject.name);
            });
        }

        public override void Clear()
        {
        }
    }
}
