using Cysharp.Threading.Tasks;
using RPG.Combat;
using RPG.Core;
using RPG.Core.Data;
using RPG.Core.Manager;
using RPG.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] bool isSpawnAreaDebugging;
    [SerializeField] int maxSpawnCount;
    [SerializeField, Range(1, 100)] float spawnAreaWidth = 10f;
    [SerializeField, Range(1,10)] float spawnAreaOffset = 0.2f;
    private int currentStage;
    private float spawnAfterElapsedTime = float.MaxValue;

    private Cell[,] grid;
    private GridController gridController;
    private List<SpawnEnemyInfo> currentSpawnInfos;
    private StageData currentStageData;
    private Camera mainCam;

    // 캐싱
    private Dictionary<int, StageData> stageData;
    private Dictionary<int, EnemyData> enemyData;
    private Dictionary<int, Enemy> enemyPrefab;

    private Dictionary<int, ObjectPooler<Enemy>> poolers;


    private Vector2 minBound;
    private Vector2 maxBound;
    float maxX;
    float maxY;
    float minX;
    float minY;

    public int LiveMonsterCount {get
        {
            return poolers.Values.Sum(x => x.ActiveCount);
        }
    }


    private void Start()
    {
        mainCam = Camera.main;
        currentStage = 1;
        poolers = new Dictionary<int, ObjectPooler<Enemy>>();
        gridController = FindObjectOfType<GridController>();
        var gridSize = gridController.GridSize;
        var gridCellSize = gridController.CellRadius * 2;

        Vector2 startPoint = gridController.GridStartPoint;
        Vector2 endPoint = new Vector2(startPoint.x + (100 * gridCellSize), startPoint.y + (100 * gridCellSize));

        minBound = startPoint;
        maxBound = endPoint;
        maxX = maxBound.x - spawnAreaOffset;
        maxY = maxBound.y - spawnAreaOffset;
        minX = minBound.x + spawnAreaOffset;
        minY = minBound.y + spawnAreaOffset;


        enemyData = Managers.Instance.Data.EnemyDataDict;
        stageData = Managers.Instance.Data.StageDataDict;
        LoadEnemyPrefabs();

        ChangeStage(currentStage);
    }

    private void LoadEnemyPrefabs()
    {
        enemyPrefab = new Dictionary<int, Enemy>();
        foreach (var i in enemyData)
        {
            enemyPrefab.Add(i.Key, ResourceCache.Load<Enemy>(i.Value.PrefabPath));
        }
    }

    bool spawnStarted = false;

    public async UniTaskVoid SpawnTask(int delayMilliSecond)
    {
        spawnStarted = true;
        await UniTask.Delay(delayMilliSecond, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
        while (true)
        {
            if(spawnAfterElapsedTime > currentStageData.SpawnRate)
            {
                if(maxSpawnCount > LiveMonsterCount)
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

    private void SetEnemyObjectPool()
    {
        poolers.Clear();

        for(int i = 0; i < currentSpawnInfos.Count; ++i)
        {
            int id = currentSpawnInfos[i].Id;
            var prefab = enemyPrefab[id];
            poolers.Add(id, new ObjectPooler<Enemy>(prefab));
        }
        
    }

    private async UniTaskVoid SpawnEnemy()
    {
        int max = currentSpawnInfos.Count;
        int randomEnemyId = currentSpawnInfos[Random.Range(0, max)].Id;
        var enemy = poolers[randomEnemyId].Get();
        enemy.GetComponent<Enemy>().SetData(enemyData[randomEnemyId]);

        Vector3 enemySpawnPos = await GetRandomPositionFromSpawnArea();

        enemy.transform.position = enemySpawnPos;
    }

    public async UniTask<Vector3> GetRandomPositionFromSpawnArea()
    {
        int random = Random.Range(1, 5);

        Vector3 topLeft, topRight;
        Vector3 bottomLeft, bottomRight;

        //Ray topLeftRay =        mainCam.ViewportPointToRay(new Vector3(0 - spawnAreaOffset, 1 + spawnAreaOffset, 0));
        //Ray topRightRay =       mainCam.ViewportPointToRay(new Vector3(1 + spawnAreaOffset, 1 + spawnAreaOffset, 0));
        //Ray bottomLeftRay =     mainCam.ViewportPointToRay(new Vector3(0 - spawnAreaOffset, 0 - spawnAreaOffset, 0));
        //Ray bottomRightRay =    mainCam.ViewportPointToRay(new Vector3(1 + spawnAreaOffset, 0 - spawnAreaOffset, 0));

        //RaycastHit hit;
        //Physics.Raycast(topLeftRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        //topLeft = hit.collider != null ? hit.point : new Vector3(minBound.x, 0, maxBound.y);

        //Physics.Raycast(topRightRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        //topRight = hit.collider != null ? hit.point : new Vector3(maxBound.x, 0, maxBound.y);

        //Physics.Raycast(bottomLeftRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        //bottomLeft = hit.collider != null ? hit.point : new Vector3(minBound.x, 0, minBound.y);

        //Physics.Raycast(bottomRightRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        //bottomRight = hit.collider != null ? hit.point : new Vector3(maxBound.x, 0, minBound.y);

        // 1 : 위     
        // 2 : 오른쪽  
        // 3 : 아래   
        // 4 : 왼쪽   

        Vector3 randomPos = random switch
        {
            1 => new Vector3(Random.Range(minX, maxX), 0, Random.Range(maxY- spawnAreaWidth, maxY)),
            2 => new Vector3(Random.Range(maxX- spawnAreaWidth, maxX), 0, Random.Range(minY, maxY)),

            3 => new Vector3(Random.Range(minX, maxX), 0, Random.Range(minY, minY + spawnAreaWidth)),
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
    public void ChangeStage(int stageNum)
    {
        currentStage = stageNum;
        currentStageData = stageData[stageNum];
        currentSpawnInfos = currentStageData.EnemyInfoList;
        maxSpawnCount = currentStageData.MaxSpawnCount;
        SetEnemyObjectPool();
    }
}
