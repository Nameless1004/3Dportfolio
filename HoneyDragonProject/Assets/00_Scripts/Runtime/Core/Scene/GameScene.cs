using Cinemachine;
using RPG.Combat;
using RPG.Core.Manager;
using RPG.Core.UI;
using RPG.Util;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core.Scene
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
        // [field: SerializeField] public MinimapCameraMovement MinimapCamera { get; set; }
        [field: SerializeField] public CinemachineVirtualCamera MainVirtualCam { get; set; }
        [field: SerializeField] public CinemachineVirtualCamera CharacterFaceCam { get; set; }

        private MonsterSpawner monsterSpawner;
        public MonsterSpawner MonsterSpawner => monsterSpawner;

        public event Action<float> OnTimeChanged;
        public event Action OnGameClear;
        public float GameElapsedTime => gameElapsedTime;
        private float gameElapsedTime = 0f;
        private bool isStopTimer = false;
        
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
            monsterSpawner = FindObjectOfType<MonsterSpawner>();
            currentPlayer = CreatePlayer(Managers.Instance.Game.selectedCharacterId);
            currentPlayer.GetComponent<Health>().OnDie += StopTimer;
            
            Managers.Instance.Stage.InitStage(1);
            monsterSpawner.OnStageChanged(1);
            monsterSpawner.SetMapBoundData(Managers.Instance.Stage.CurrentStage.Grid);

            // UI Scene 로딩
            SceneManager.LoadScene((int)SceneType.UI, LoadSceneMode.Additive);

            Managers.Instance.Sound.PlaySound(SoundType.BGM, "Sound/BGM");

            // 2초 뒤 생성
            OnTimeChanged += monsterSpawner.OnTimeChanged;
            monsterSpawner.SpawnTask(2000).Forget();
        }

        private Player CreatePlayer(int playerModelId)
        {
            var playerData = Managers.Instance.Data.PlayerDataDict[playerModelId];
            var playerExpTable = Managers.Instance.Data.PlayerExpDataDict;
            var playerPrefab = ResourceCache.Load<Player>(playerData.PrefabPath);

            var currentPlayer = MonoBehaviour.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            currentPlayer.InitializePlayer(playerData, playerExpTable);


            MainCamera = Camera.main;

            MainVirtualCam.transform.position = currentPlayer.transform.position + (Vector3.up * 30f);
            MainVirtualCam.Follow = currentPlayer.transform;

            //MinimapCamera.SetPlayer(currentPlayer.transform);
            return currentPlayer;
        }

        private void Update()
        {
            if(isStopTimer == false)
            {
                gameElapsedTime += Time.deltaTime;
                OnTimeChanged?.Invoke(gameElapsedTime);
            }
            if(Input.GetKeyDown(KeyCode.Q)) 
            {
                MainVirtualCam.gameObject.SetActive(false);
                CharacterFaceCam.gameObject.SetActive(true);
                CharacterFaceCam.transform.position = currentPlayer.position + currentPlayer.forward * 3f + Vector3.up * 1.5f;
                CharacterFaceCam.LookAt = currentPlayer.transform;
            }

        }

        private void StopTimer()
        {
            isStopTimer = true;
        }

        public void GameClear()
        {
            Debug.Log("GameClear");
            OnGameClear?.Invoke();
        }
    }
}
