using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core.Manager
{
    [Serializable]
    public class GameManager : IManager
    {
        public Transform PoolTransform;

        public void GameStart()
        {
            SceneManager.LoadScene(1);
        }


        public void ClearPoolTransform()
        {
            if(PoolTransform is not null) PoolTransform.DestroyChild();
        }

        public void Init()
        {
            if (PoolTransform is not null) PoolTransform.DestroyChild();
            GameObject go = new GameObject("PoolTransform");
            PoolTransform = go.transform;
            MonoBehaviour.DontDestroyOnLoad(go);
        }
    }
}
