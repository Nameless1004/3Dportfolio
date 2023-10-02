using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class PlayerExpView : MonoBehaviour
    {
        public Image ProgressBar;

        public void UpdateExpBar(float ratio)
        {
            ProgressBar.fillAmount = ratio;
        }
    }
}
