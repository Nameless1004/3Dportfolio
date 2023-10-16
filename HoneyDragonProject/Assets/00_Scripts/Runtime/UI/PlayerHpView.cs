using TMPro;
using UnityEngine;

public class PlayerHpView : MonoBehaviour
{
    public RectTransform ProgressBar;
    public TMP_Text Text;

    public void OnHealthChanged(float ratio)
    {
        int ratioToInt = (int)(ratio * 100f);
        Text.text = $"{ratioToInt}%";
        SetProgressBar(ratio);
    }

    private void SetProgressBar(float ratio)
    {
        var anchorMax = ProgressBar.anchorMax;
        anchorMax.x = ratio;
        ProgressBar.anchorMax = anchorMax;
    }
}
