using RPG.Combat;
using RPG.Core;
using RPG.Core.Data;
using RPG.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private int currentStage;
    private float spawnAfterElapsedTime = float.MaxValue;

    private List<SpawnEnemyInfo> currentSpawnInfos;
    private StageData currentStageData;

    // 캐싱
    private Dictionary<int, StageData> stageData;
    private Dictionary<int, EnemyData> enemyData;
    private Dictionary<int, GameObject> enemyPrefab;

    private int liveMonsterCount;

    private void Start()
    {
        currentStage = 1;

        enemyData = Managers.Instance.Data.EnemyDataDict;
        LoadEnemyPrefabs();
        stageData = Managers.Instance.Data.StageDataDict;

        ChangeStage(currentStage);
    }

    private void LoadEnemyPrefabs()
    {
        enemyPrefab = new Dictionary<int, GameObject>();
        foreach (var i in enemyData)
        {
            enemyPrefab.Add(i.Key, Resources.Load<GameObject>(i.Value.PrefabPath));
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
        var enemy = Instantiate(enemyPrefab[randomEnemyId]);
        enemy.GetComponent<Enemy>().SetData(enemyData[randomEnemyId]);
        Player player = FindAnyObjectByType<Player>();
        // Vector2 rand = Random.insideUnitCircle * 20f;
        // Vector2 pos = new Vector2(player.position.x, player.position.z) + rand;
        Vector3 enemySpawnPos = GetRandomPosition();
        enemy.transform.position = enemySpawnPos;
        enemy.GetComponent<Health>().OnDie += () => { liveMonsterCount--; };
    }

    public Vector3 GetRandomPosition()
    {
        int random = Random.Range(1, 5);
        Debug.Log(random);
        float offset = 0.2f;
        float width = 1.08f;
        float height = 1.92f;
        float z = 10f;
        // 1 : 위     
        // 2 : 오른쪽  
        // 3 : 아래   
        // 4 : 왼쪽   
        Vector3 randomPos = random switch
        {
            1 => new Vector3(Random.Range(0 - (offset + width), 1 + offset + width), Random.Range(1 + offset, 1 + offset + height), z),
            3 => new Vector3(Random.Range(0 - (offset + width), 1 + offset + width), Random.Range(0 - (offset + height), 0 - offset), z),

            2 => new Vector3(Random.Range(1 + offset, 1 + offset + width), Random.Range(0 - (offset + height), 1 + offset + height), z),
            4 => new Vector3(Random.Range(0 - (offset + width), 0 - offset), Random.Range(0 - (offset + height), 1 + offset + height), z),
            _ => Vector3.zero
        };

        Debug.Log($"RandomPos : {randomPos}");
        Vector3 vtw = Camera.main.ViewportToWorldPoint(randomPos);
        return vtw;
    }

    // 스테이지가 바뀔 때 소환되는 적 없애기
    public void ChangeStage(int stageNum)
    {
        currentStage = stageNum;
        currentStageData = stageData[stageNum];
        currentSpawnInfos = currentStageData.EnemyInfoList;
    }
}
