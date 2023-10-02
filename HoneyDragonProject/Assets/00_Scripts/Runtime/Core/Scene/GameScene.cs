using Cinemachine;
using RPG.Core.Manager;
using RPG.Core.Scene;
using RPG.Core.UI;
using RPG.Util;
using UnityEngine;

namespace RPG.Core
{
    public class GameScene : BaseScene
    {
        public Player player;
        public PlayerExpPresenter expPresenter;
        public Camera mainCamera;
        public CinemachineVirtualCamera mainVirtualCam;

        
        public override void Clear()
        {
            Managers.Instance.Game.GameScene = null;
        }

        public override void Init()
        {
            Managers.Instance.Game.GameScene = this;
            player = CreatePlayer();
            expPresenter.SetModel(player);
        }

        private Player CreatePlayer()
        {
            var playerData = Managers.Instance.Data.PlayerData;
            var playerExpTable = Managers.Instance.Data.PlayerExpDataDict;
            var playerPrefab = ResourceCache.Load<Player>(playerData.PrefabPath);

            // 플레이어 시작 위치
            var startPosition = GameObject.FindWithTag("StartPosition");

            var currentPlayer = MonoBehaviour.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            currentPlayer.InitializePlayer(playerData, playerExpTable);
            mainCamera = Camera.main;
            var vcams = MonoBehaviour.FindObjectsOfType<CinemachineVirtualCamera>(true);
            mainVirtualCam = vcams[0];
            vcams[0].gameObject.SetActive(true);
            vcams[1].gameObject.SetActive(false);

            mainVirtualCam.transform.position = currentPlayer.transform.position + (Vector3.up * 30f);
            mainVirtualCam.Follow = currentPlayer.transform;
            return currentPlayer;
        }
    }
}
