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
    [SerializeField, Range(0,1)] float spawnAreaOffset = 0.2f;
    private int currentStage;
    private float spawnAfterElapsedTime = float.MaxValue;

    private List<SpawnEnemyInfo> currentSpawnInfos;
    private StageData currentStageData;
    private Camera mainCam;

    // 캐싱
    private Dictionary<int, StageData> stageData;
    private Dictionary<int, EnemyData> enemyData;
    private Dictionary<int, Enemy> enemyPrefab;

    private Dictionary<int, ObjectPooler<Enemy>> poolers;
    private int liveMonsterCount;

    private void Start()
    {
        mainCam = Camera.main;
        currentStage = 1;
        poolers = new Dictionary<int, ObjectPooler<Enemy>>();

        enemyData = Managers.Instance.Data.EnemyDataDict;
        LoadEnemyPrefabs();
        stageData = Managers.Instance.Data.StageDataDict;

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (spawnStarted == true) return;

            StartCoroutine(SpawnCoroutine());
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            poolers.Clear();
        }
    }

    IEnumerator SpawnCoroutine()
    {
        spawnStarted = true;
        while (true)
        {
            if(spawnAfterElapsedTime > currentStageData.SpawnRate)
            {
                spawnAfterElapsedTime = 0f;
                SpawnEnemy();
            }
            else
            {
                spawnAfterElapsedTime += Time.deltaTime;
            }
            yield return null;
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

    private void SpawnEnemy()
    {
        if(liveMonsterCount >= currentStageData.MaxSpawnCount)
        {
            spawnAfterElapsedTime = float.MaxValue;
            return;
        }

        liveMonsterCount++;
        int max = currentSpawnInfos.Count;
        int randomEnemyId = currentSpawnInfos[Random.Range(0, max)].Id;
        var enemy = poolers[randomEnemyId].Get();
        enemy.GetComponent<Enemy>().SetData(enemyData[randomEnemyId]);
        Player player = FindAnyObjectByType<Player>();

        Vector3 enemySpawnPos = GetRandomPositionFromSpawnArea();
        enemy.transform.position = enemySpawnPos;
        enemy.GetComponent<Health>().OnDie += () => { liveMonsterCount--; };
    }

    public Vector3 GetRandomPositionFromSpawnArea()
    {
        int random = Random.Range(1, 5);

        Vector3 topLeft, topRight;
        Vector3 bottomLeft, bottomRight;

        Ray topLeftRay =        mainCam.ViewportPointToRay(new Vector3(0 - spawnAreaOffset, 1 + spawnAreaOffset, 0));
        Ray topRightRay =       mainCam.ViewportPointToRay(new Vector3(1 + spawnAreaOffset, 1 + spawnAreaOffset, 0));
        Ray bottomLeftRay =     mainCam.ViewportPointToRay(new Vector3(0 - spawnAreaOffset, 0 - spawnAreaOffset, 0));
        Ray bottomRightRay =    mainCam.ViewportPointToRay(new Vector3(1 + spawnAreaOffset, 0 - spawnAreaOffset, 0));

        RaycastHit hit;
        Physics.Raycast(topLeftRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        topLeft = hit.point;

        Physics.Raycast(topRightRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        topRight = hit.point;

        Physics.Raycast(bottomLeftRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        bottomLeft = hit.point;

        Physics.Raycast(bottomRightRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        bottomRight = hit.point;

        // 1 : 위     
        // 2 : 오른쪽  
        // 3 : 아래   
        // 4 : 왼쪽   
        Vector3 randomPos = random switch
        {
            1 => new Vector3(Random.Range(topLeft.x - spawnAreaWidth, topRight.x + spawnAreaWidth),   0,  Random.Range(topRight.z, topRight.z + spawnAreaWidth)),
            3 => new Vector3(Random.Range(topLeft.x - spawnAreaWidth, topRight.x + spawnAreaWidth),   0,  Random.Range(bottomRight.z - spawnAreaWidth, bottomRight.z)),

            2 => new Vector3(Random.Range(topRight.x, topRight.x + spawnAreaWidth),          0,  Random.Range(bottomRight.z - spawnAreaWidth, topRight.z + spawnAreaWidth)),
            4 => new Vector3(Random.Range(topLeft.x - spawnAreaWidth, topLeft.x),          0,  Random.Range(bottomRight.z - spawnAreaWidth, topRight.z + spawnAreaWidth)),
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

        Ray topLeftRay = mainCam.ViewportPointToRay(new Vector3(0 - spawnAreaOffset, 1 + spawnAreaOffset, 0));
        Ray topRightRay = mainCam.ViewportPointToRay(new Vector3(1 + spawnAreaOffset, 1 + spawnAreaOffset, 0));
        Ray bottomLeftRay = mainCam.ViewportPointToRay(new Vector3(0 - spawnAreaOffset, 0 - spawnAreaOffset, 0));
        Ray bottomRightRay = mainCam.ViewportPointToRay(new Vector3(1 + spawnAreaOffset, 0 - spawnAreaOffset, 0));

        RaycastHit hit;
        Physics.Raycast(topLeftRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        topLeft = hit.point;

        Physics.Raycast(topRightRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        topRight = hit.point;

        Physics.Raycast(bottomLeftRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        bottomLeft = hit.point;

        Physics.Raycast(bottomRightRay, out hit, 2000f, LayerMask.GetMask("Ground"));
        bottomRight = hit.point;

        newtopLeft = new Vector3(topLeft.x - spawnAreaWidth, 0, topLeft.z + spawnAreaWidth);
        newtopRight = new Vector3(topRight.x + spawnAreaWidth,0, topRight.z + spawnAreaWidth);
        newbottomLeft = new Vector3(bottomLeft.x - spawnAreaWidth, 0,bottomLeft.z - spawnAreaWidth);
        newbottomRight = new Vector3(bottomRight.x + spawnAreaWidth,0, bottomRight.z - spawnAreaWidth);

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
        SetEnemyObjectPool();
    }
}
