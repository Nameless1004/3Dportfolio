using System;
using TMPro;
using UnityEngine;

namespace RPG.Core.UI
{
    public class StageElapsedTimeView : MonoBehaviour
    {
        private TMP_Text text;
        private void Awake()
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        public void UpdateElapsedTime(float elapsedTime)
        {
            TimeSpan span = TimeSpan.FromSeconds(elapsedTime);
            text.text = $"{span.Minutes:D2} : {span.Seconds:D2}";
        }
    }
}
