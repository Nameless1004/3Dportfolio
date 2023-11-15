using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Core.UI
{
    public class StartGameUI : Popup
    {
        private CanvasGroup group;
        public override CanvasGroup CanvasGroup { get => group; }
        public UnityEvent OnClosed;

        public TMP_Text SelectedCharacterName;
        public string SelectCharacterPrefabPath;
        public List<CharacterSelectButton> characterButtonList;

        private void Awake()
        {
            group = GetComponent<CanvasGroup>();
            characterButtonList = GetComponentsInChildren<CharacterSelectButton>().ToList();
            characterButtonList.ForEach(button => button.OnButtoClicked = OnCharacterSelected);
            characterButtonList[0].OnButtoClicked.Invoke(characterButtonList[0]);
        }

        private void OnCharacterSelected(CharacterSelectButton selectedButton)
        {
            foreach(var button in characterButtonList)
            {
                if(button != selectedButton)
                {
                    button.Group.alpha = 0.5f;
                }
            }
            SelectedCharacterName.text = selectedButton.modelName;
            selectedButton.Group.alpha = 1f;
            SelectCharacterPrefabPath = selectedButton.modelPrefabPath;
        }

        public override void Close()
        {
            SimpleFadeOut(FadeInTime).Forget();
        }


        public override void Open()
        {
            SimpleFadeIn(FadeOutTime).Forget();
        }

        public async override UniTaskVoid SimpleFadeOut(float fadeTime)
        {
            float elapsedTime = 0f;
            CanvasGroup.alpha = 1f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float ratio = 1f - elapsedTime / fadeTime;
                CanvasGroup.alpha = ratio;
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }
            CanvasGroup.alpha = 0f;
            OnClosed?.Invoke();
        }
    }
}
