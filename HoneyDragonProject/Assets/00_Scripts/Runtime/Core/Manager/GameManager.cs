using RPG.Core.Scene;
using System;
using UnityEngine;

namespace RPG.Core.Manager
{
    [Serializable]
    public class GameManager : IManager
    {
        public int selectedCharacterId;

        private GameScene gameScene;
        public GameScene GameScene{ 
            get 
            {
                if(gameScene == null)
                {
                    gameScene = Transform.FindObjectOfType<GameScene>();
                }
                return gameScene;   
            } 
            set
            {
                gameScene = value;
            }
        }

        public Player CurrentPlayer => GameScene.Player;

        public void Init()
        {
        }

        public void Clear()
        {
        }

        public void GameClear()
        {
            GameScene.GameClear();
        }
    }
}
