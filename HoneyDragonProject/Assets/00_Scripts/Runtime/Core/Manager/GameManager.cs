using Cinemachine;
using RPG.Util;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core.Manager
{
    [Serializable]
    public class GameManager : IManager
    {
        public GameScene GameScene{get; set;}

        public void GameStart()
        {
            SceneManager.LoadScene(1);
        }


        public void Init()
        {
        }

        public void Clear()
        {

        }
    }
}
