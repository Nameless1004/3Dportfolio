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
        DataManager data;
        Player playerPrefab;

        public Camera mainCamera { get; private set; }
        public CinemachineVirtualCamera virtualCamera { get; private set; }
        public Player CurrentPlayer { get; private set; }

        public Player CreatePlayer()
        {
            if (CurrentPlayer != null) return CurrentPlayer;

            if(playerPrefab == null)
            {
                playerPrefab = ResourceCache.Load<Player>(data.PlayerData.PrefabPath);    
            }

            // 플레이어 시작 위치
            var startPosition = GameObject.FindWithTag("StartPosition");


            CurrentPlayer = MonoBehaviour.Instantiate(playerPrefab, startPosition.transform.position, Quaternion.identity);
            CurrentPlayer.InitializePlayer(data);
            mainCamera = Camera.main;
            var vcams = MonoBehaviour.FindObjectsOfType<CinemachineVirtualCamera>(true);
            virtualCamera = vcams[0];
            vcams[0].gameObject.SetActive(true);
            vcams[1].gameObject.SetActive(false);

            virtualCamera.transform.position = CurrentPlayer.transform.position + (Vector3.up * 30f);
            virtualCamera.Follow = CurrentPlayer.transform;
            return CurrentPlayer;
        }

        public void GameStart()
        {
            SceneManager.LoadScene(1);
        }


        public void Init()
        {
            data = Managers.Instance.Data;
        }
    }
}
