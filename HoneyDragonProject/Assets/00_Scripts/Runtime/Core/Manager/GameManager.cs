using RPG.Core.Scene;
using System;
using UnityEngine;

namespace RPG.Core.Manager
{
    [Serializable]
    public class GameManager : IManager
    {
        public int selectedCharacterId;

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
