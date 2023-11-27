using RPG.Core;
using RPG.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Manager
{
    public class StageManager : IManager
    {
        Dictionary<int, StageData> stageDataDict;

        public int CurrentStageNum { get; private set; }
        public Stage CurrentStage { get; private set; }
        public StageData GetStageData(int stageNum) => stageDataDict[stageNum];
        public StageData CurrentStageData => stageDataDict[CurrentStageNum];
        public event Action<int> OnStageChanged;


        private void SetStage(int stageNum)
        {
            Clear();
            CurrentStageNum = stageNum;
            if (CurrentStage != null)
            {
                UnityEngine.Object.Destroy(CurrentStage);
            }
            CurrentStage = MonoBehaviour.Instantiate(Util.ResourceCache.Load<Stage>(stageDataDict[CurrentStageNum].Prefab));
            Managers.Instance.Game.CurrentPlayer.position = CurrentStage.StartPosition.position;
            OnStageChanged?.Invoke(CurrentStageNum);
        }

        public void NextStage()
        {
            SetStage(CurrentStageNum + 1);
        }

        public void Init()
        {
            stageDataDict = Managers.Instance.Data.StageDataDict;
        }

        public void InitStage(int stageNum)
        {
            SetStage(stageNum);
        }

        public void Clear()
        {

        }
    }
}
