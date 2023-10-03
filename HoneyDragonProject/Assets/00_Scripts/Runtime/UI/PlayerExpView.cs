using RPG.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class PlayerExpView : BaseUI
    {
        public Image ProgressBar;

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Init()
        {
          
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public void UpdateExpBar(float ratio)
        {
            ProgressBar.fillAmount = ratio;
        }
    }
}
