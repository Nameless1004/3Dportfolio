using RPG.Core;
using RPG.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class PlayerExpPresenter : MonoBehaviour
    {
        Player player;

        void Start()
        {
            player = Managers.Instance.Game.GameScene.player;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
