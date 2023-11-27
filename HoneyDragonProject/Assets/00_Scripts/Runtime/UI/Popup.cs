using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Popup : MonoBehaviour
    {
        public float FadeInTime = 1f;
        public float FadeOutTime = 1f;
        protected float elapsedTime = 0f;
        public bool isFading;

        public abstract CanvasGroup CanvasGroup {get;}
        public Action OnFadeinDone;
        public Action OnFadeoutDone;

        public abstract void Open();
        public abstract void Close();

        public async virtual UniTaskVoid SimpleFadeIn(float fadeTime)
        {
            if (isFading) return;
            isFading = true;
            float elapsedTime = 0f;
            CanvasGroup.alpha = 0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float ratio = (elapsedTime / fadeTime);
                CanvasGroup.alpha = ratio;
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }
            CanvasGroup.alpha = 1f;
            OnFadeinDone?.Invoke();
            isFading = false;
        }

        public async virtual UniTaskVoid SimpleFadeOut(float fadeTime)
        {
            if (isFading) return;
            isFading = true;
            float elapsedTime = 0f;
            CanvasGroup.alpha = 1f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float ratio = 1f - (elapsedTime / fadeTime);
                CanvasGroup.alpha = ratio;
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }
            CanvasGroup.alpha = 0f;
            OnFadeoutDone?.Invoke();
            isFading = false;
        }
    }
}
