using Cysharp.Threading.Tasks;
using RPG.Combat;
using RPG.Core;
using RPG.Core.Data;
using RPG.Core.Manager;
using RPG.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] bool isSpawnAreaDebugging;
    [SerializeField] int maxSpawnCount;
    [SerializeField, Range(1, 100)] float spawnAreaWidth = 10f;
    [SerializeField, Range(1,10)] float spawnAreaOffset = 0.2f;

    private float spawnAfterElapsedTime = float.MaxValue;
    private bool isBossSpawned = false;

    private GridController gridController;
    private List<int> currentSpawnInfos;
    private List<NormalMonster> spawnedEnemies = new List<NormalMonster>();
    private Stage currentStage;
    private StageData currentStageData;

    public event Action OnBossSpawned;

    // 캐싱
    private Dictionary<int, EnemyData> enemyData;
    private Dictionary<int, EnemyData> bossData;

    private Dictionary<int, Enemy> enemyPrefab;

    private Dictionary<int, ObjectPooler<NormalMonster>> poolers;

    #region 맵 범위 관련 필드
    private Vector2 minBound;
    private Vector2 maxBound;
    float maxX;
    float maxY;
    float minX;
    float minY;
    #endregion

    public int LiveMonsterCount {get
        {
            return poolers.Values.Sum(x => x.ActiveCount);
        }
    }

    private void Awake()
    {
        poolers = new Dictionary<int, ObjectPooler<NormalMonster>>();

        enemyData = Managers.Instance.Data.EnemyDataDict;
        bossData = Managers.Instance.Data.BossDataDict;

        LoadEnemyPrefabs();
    }

    private void OnEnable()
    {
        Managers.Instance.Stage.OnStageChanged += OnStageChanged;
    }

    private void OnDisable()
    {
        Managers.Instance.Stage.OnStageChanged -= OnStageChanged;
    }

    public void OnTimeChanged(float time)
    {
        if(isBossSpawned == false && time > currentStageData.BossSpawnTime)
        {
            isBossSpawned = true;
            // 보스가 등장할 때 처리를 위한 이벤트
            OnBossSpawned?.Invoke();
            int a = currentStageData.BossId;
            var bossPrefab = Resources.Load(Managers.Instance.Data.BossDataDict[a].PrefabPath);
            if (bossPrefab != null)
            {
                var randomPos = GetRandomPositionInsideSpawnArea();
                Instantiate(bossPrefab, randomPos, Quaternion.identity);
            }
        }
    }

    public void SetMapBoundData(GridController grid)
    {
        gridController = grid;
        var gridCellSize = gridController.CellRadius * 2;

        Vector2 startPoint = gridController.GridStartPoint;
        Vector2 endPoint = new Vector2(startPoint.x + (100 * gridCellSize), startPoint.y + (100 * gridCellSize));

        minBound = startPoint;
        maxBound = endPoint;
        maxX = maxBound.x - spawnAreaOffset;
        maxY = maxBound.y - spawnAreaOffset;
        minX = minBound.x + spawnAreaOffset;
        minY = minBound.y + spawnAreaOffset;
    }

    private void LoadEnemyPrefabs()
    {
        enemyPrefab = new Dictionary<int, Enemy>();
        foreach (var i in enemyData)
        {
            enemyPrefab.Add(i.Key, ResourceCache.Load<Enemy>(i.Value.PrefabPath));
        }
    }

    private void SetEnemyObjectPool()
    {
        ClearEnemyPool();

        for (int i = 0; i < currentSpawnInfos.Count; ++i)
        {
            int id = currentSpawnInfos[i];
            var prefab = enemyPrefab[id] as NormalMonster;
            poolers.Add(id, new ObjectPooler<NormalMonster>(prefab, this.transform));
        }
    }

    private void ClearEnemyPool()
    {

        // 소환된 적들을 오브젝트 풀로 보낸다.
        foreach (var i in spawnedEnemies)
        {
            poolers[i.Data.Id].Release(i);
        }

        // 오브젝트 풀에서 일괄 클리어 시킴.
        foreach (var enemy in poolers)
        {
            enemy.Value.Clear();
        }

        spawnedEnemies.Clear();
        poolers.Clear();
    }

    public int KillCount { get; set; }

    public void OnDieEnemy()
    {
        KillCount++;
    }

    public async UniTaskVoid SpawnTask(int delayMilliSecond)
    {
        await UniTask.Delay(delayMilliSecond, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
        while (true)
        {
            if (spawnAfterElapsedTime > currentStageData.SpawnRate)
            {
                if (maxSpawnCount > LiveMonsterCount)
                {
                    spawnAfterElapsedTime = 0f;
                    SpawnEnemy().Forget();
                }
                else
                {
                    await UniTask.Yield(this.GetCancellationTokenOnDestroy());
                }
            }
            else
            {
                spawnAfterElapsedTime += Time.deltaTime;
            }
            await UniTask.Yield(this.GetCancellationTokenOnDestroy());
        }
    }

    private async UniTaskVoid SpawnEnemy()
    {
        int max = currentSpawnInfos.Count;
        int randomEnemyId = currentSpawnInfos[Random.Range(0, max)];

        if (poolers.ContainsKey(randomEnemyId) == false) return;
        if (poolers[randomEnemyId].TryGet(out var enemy) == false) return;

        spawnedEnemies.Add(enemy);
        enemy.SetData(enemyData[randomEnemyId]);
        enemy.Controller.SetTarget(Managers.Instance.Game.CurrentPlayer);
        enemy.name = enemy.Data.Id.ToString() + "_" + poolers[randomEnemyId].CountAll;

        Health enemyHealth = enemy.GetComponent<Health>();
        enemyHealth.OnDie -= OnDieEnemy;
        enemyHealth.OnDie += OnDieEnemy;
        int playerLevel = Managers.Instance.Game.CurrentPlayer.GetComponent<Player>().Status.Level;
        int enemyhp = (int)(enemy.Data.Hp * Mathf.Pow(playerLevel, 0.6f));
        enemyHealth.SetHp(enemyhp);

        Vector3 enemySpawnPos = GetRandomPositionInsideSpawnArea();

        enemy.transform.position = enemySpawnPos;
    }

    public Vector3 GetRandomPositionInsideSpawnArea()
    {
        int random = Random.Range(1, 5);

        Vector3 randomPos = random switch
        {
            1 => new Vector3(Random.Range(minX, maxX), 0, Random.Range(maxY- spawnAreaWidth, maxY)),
            3 => new Vector3(Random.Range(minX, maxX), 0, Random.Range(minY, minY + spawnAreaWidth)),

            2 => new Vector3(Random.Range(maxX- spawnAreaWidth, maxX), 0, Random.Range(minY, maxY)),
            4 => new Vector3(Random.Range(minX, minX + spawnAreaWidth), 0,  Random.Range(minY, maxY)),
            _ => Vector3.zero
        };

        return randomPos;
    }

    private void OnDrawGizmos()
    {
        if (isSpawnAreaDebugging == false) return;

        Vector3 topLeft, topRight;
        Vector3 bottomLeft, bottomRight;

        Vector3 newtopLeft, newtopRight;
        Vector3 newbottomLeft, newbottomRight;


        topLeft = new Vector3(minX, 0, maxY);
        topRight = new Vector3(maxX, 0, maxY);
        bottomLeft = new Vector3(minX, 0, minY);
        bottomRight = new Vector3(maxX, 0, minY);

        newtopLeft = new Vector3(topLeft.x + spawnAreaWidth, 0, topLeft.z - spawnAreaWidth);
        newtopRight = new Vector3(topRight.x - spawnAreaWidth,0, topRight.z - spawnAreaWidth);

        newbottomLeft = new Vector3(bottomLeft.x + spawnAreaWidth, 0,bottomLeft.z + spawnAreaWidth);
        newbottomRight = new Vector3(bottomRight.x - spawnAreaWidth,0, bottomRight.z + spawnAreaWidth);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(newtopLeft,     newtopRight);
        Gizmos.DrawLine(newtopRight,    newbottomRight);
        Gizmos.DrawLine(newbottomRight, newbottomLeft);
        Gizmos.DrawLine(newbottomLeft,  newtopLeft);
    }

    // 스테이지가 바뀔 때 소환되는 적 없애기
    public void OnStageChanged(int stageNum)
    {
        currentStageData = Managers.Instance.Stage.GetStageData(stageNum);
        currentStage = Managers.Instance.Stage.CurrentStage;
        currentSpawnInfos = currentStageData.SpawnEnemyList;
        maxSpawnCount = currentStageData.MaxSpawnCount;
        isBossSpawned = false;
        SetEnemyObjectPool();
    }
}
