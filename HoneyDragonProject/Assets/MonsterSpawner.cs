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
        Vector2 rand = Random.insideUnitCircle * 20f;
        Vector2 pos = new Vector2(player.position.x, player.position.z) + rand;
        Vector3 enemySpawnPos = new Vector3(pos.x, 0, pos.y);
        enemy.transform.position = enemySpawnPos;
        enemy.GetComponent<Health>().OnDie += () => { liveMonsterCount--; };
    }

    // 스테이지가 바뀔 때 소환되는 적 없애기
    public void ChangeStage(int stageNum)
    {
        currentStage = stageNum;
        currentStageData = stageData[stageNum];
        currentSpawnInfos = currentStageData.EnemyInfoList;
    }
}
