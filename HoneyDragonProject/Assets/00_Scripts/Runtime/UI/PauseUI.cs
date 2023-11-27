using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core.UI
{
    public class PauseUI : Popup
    {
        private CanvasGroup group;
        public override CanvasGroup CanvasGroup => group;

        private void Awake()
        {
            group = GetComponent<CanvasGroup>();    
        }

        public override void Close()
        {
            Time.timeScale = 1f;
            SimpleFadeOut(0.5f).Forget();
        }

        public override void Open()
        {
            Time.timeScale = 0f;
            
            SimpleFadeIn(0.5f).Forget();
        }

        public void OnQuitButtonClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();

        }

        public void OnRestartButtonClicked()
        {
            Close();
            SceneManager.LoadScene((int)Scene.SceneType.Lobby);
        }

        public void OnResumeButtonClicked()
        {
            Close();
        }
    }
}
