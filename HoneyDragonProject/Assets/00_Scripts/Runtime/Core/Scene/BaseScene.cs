using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Scene
{
    public enum SceneType
    {
        Unknown,
        Lobby,
        Game,
        UI,
    }

    public abstract class BaseScene : MonoBehaviour
    {
        public SceneType SceneType { get; private set; } = SceneType.Unknown;
        protected bool initialized = false;
        private void Awake()
        {
            Init();
        }

        public abstract void Init();
        public abstract void Clear();
    }
}
