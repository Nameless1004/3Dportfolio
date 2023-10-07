using Cinemachine;
using RPG.Core.Manager;
using RPG.Core.Scene;
using RPG.Core.UI;
using RPG.Util;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    public class GameScene : BaseScene
    {
        private Player currentPlayer;
        public Player Player { get 
            { 
                if(currentPlayer == null)
                {
                    Init();
                    initialized = true;
                }
                return currentPlayer;
            }
        }
        [field: SerializeField] public PlayerExpPresenter ExpPresenter { get; set; }    
        [field: SerializeField] public Camera MainCamera { get; set; }
        [field: SerializeField] public MinimapCameraMovement MinimapCamera { get; set; }
        [field: SerializeField] public CinemachineVirtualCamera MainVirtualCam { get; set; }

        // 플레이어가 생성될 시 호출된 이벤트
        public event Action OnPlayerCreated;
        
        public override void Clear()
        {
            Managers.Instance.Game.GameScene = null;
            Destroy(Player.gameObject);
        }

        public override void Init()
        {
            if (initialized == true) return;
            initialized = true;
            Managers.Instance.Game.GameScene = this;
            currentPlayer = CreatePlayer();
            OnPlayerCreated?.Invoke();
            
            // UI Scene 로딩
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            //ExpPresenter.SetModel(currentPlayer);
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
            MainCamera = Camera.main;
            var vcams = MonoBehaviour.FindObjectsOfType<CinemachineVirtualCamera>(true);
            MainVirtualCam = vcams[0];
            vcams[0].gameObject.SetActive(true);
            vcams[1].gameObject.SetActive(false);

            MainVirtualCam.transform.position = currentPlayer.transform.position + (Vector3.up * 30f);
            MainVirtualCam.Follow = currentPlayer.transform;

            MinimapCamera.SetPlayer(currentPlayer.transform);
            return currentPlayer;
        }
    }
}
