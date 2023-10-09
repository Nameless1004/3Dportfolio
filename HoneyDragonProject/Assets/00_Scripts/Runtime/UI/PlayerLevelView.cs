using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Core.UI
{
    public class PlayerLevelView : MonoBehaviour
    {
        TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        public void OnLevelUpdated(int level)
        {
            text.text = level.ToString();
        }
    }
}
