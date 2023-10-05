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

        public Player CurrentPlayer { get
            {
                if(GameScene == null)
                {
                    GameScene = Transform.FindObjectOfType<GameScene>();
                }
                return GameScene.Player;
            } 
        }

        public void Init()
        {
        }

        public void Clear()
        {

        }
    }
}
