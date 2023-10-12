using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Core.UI
{
    public class PlayerLevelView : MonoBehaviour
    {
        public TMP_Text Text;

        public void OnLevelUpdated(int level)
        {
            Text.text = level.ToString();
        }
    }
}
