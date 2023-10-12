using Cysharp.Threading.Tasks;
using RPG.Core.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace RPG.Core.UI
{
    public class PlayerExpView : BaseUI
    {
        public RectTransform ProgressBar;
        public TMP_Text Text;

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
            SetProgressBar(ratio);
            int ratioToInt = (int)(ratio * 100f);
            Text.text = $"{ratioToInt}%";
        }

        private void SetProgressBar(float ratio)
        {
            var anchorMax = ProgressBar.anchorMax;
            anchorMax.x = ratio;
            ProgressBar.anchorMax = anchorMax;
        }

    }
}
