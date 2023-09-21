using RPG.Core;
using RPG.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Manager
{
    public class StageManager : MonoBehaviour, IManager
    {
        List<Enemy> enemies = new List<Enemy>();
        Dictionary<int, StageData> stageDataDict;

        public int CurrentStage { get; private set; }
        public StageData CurrentStageData { get; private set; }

        #region Properties
        bool IsStageClear => enemies.Count == 0;
        #endregion

        IEnumerator SpawnEnemy()
        {
            yield return null;
        }

        public void ClearStage() { }

        public void SetStage(int stageNum)
        {
            ClearStage();
            SetEnemies(CurrentStageData.EnemyInfoList);
            // SetPlayer();
        }

        public void NextStage()
        {
            CurrentStage++;
            SetStage(CurrentStage);
        }

        public void SetEnemies(List<SpawnEnemyInfo> enemyInfoList) 
        {
            if (enemyInfoList.Count == 0) return;


        }

        public void Init()
        {
            stageDataDict = Managers.Instance.Data.StageDataDict;
        }
    }
}
