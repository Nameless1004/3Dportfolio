using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.UI
{
    public abstract class Popup : MonoBehaviour
    {
        public float FadeInTime = 1f;
        public float FadeOutTime = 1f;

        public abstract CanvasGroup CanvasGroup {get;}

        public abstract void Open();
        public abstract void Close();

        public async virtual UniTaskVoid SimpleFadeIn(float fadeTime)
        {
            float elapsedTime = 0f;
            CanvasGroup.alpha = 0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float ratio = 1f - (elapsedTime / fadeTime);
                CanvasGroup.alpha = ratio;
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }
            CanvasGroup.alpha = 1f;
        }

        public async virtual UniTaskVoid SimpleFadeOut(float fadeTime)
        {
            float elapsedTime = 0f;
            CanvasGroup.alpha = 0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float ratio = elapsedTime / fadeTime;
                CanvasGroup.alpha = ratio;
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }
            CanvasGroup.alpha = 1f;
        }
    }
}
