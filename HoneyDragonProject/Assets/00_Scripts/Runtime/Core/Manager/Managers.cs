using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core.Manager
{
    public interface IManager
    {
        public void Init();
    }

    public class Managers : Singleton<Managers>
    {
        public DataManager Data { get; private set; }

        [field:SerializeField]
        public GameManager Game { get; private set; }
        public SkillManager Skill { get; private set; }
        public StageManager Stage { get; private set; }

        private List<IManager> managers = new List<IManager>();

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Game.ClearPoolTransform();
        }

        public void Release()
        {
            managers.Clear();
        }

        private void AddManagers()
        {
            var properties = GetType().GetProperties();
            for (int i = 0; i <  properties.Length; ++i)
            {
                Type t = properties[i].PropertyType;
                var inter = t.GetInterface("IManager");
                if(inter is not null)
                {
                    Debug.Log(t.Name);
                    var instance = Activator.CreateInstance(t);
                    properties[i].SetValue(this, instance);
                    IManager manager = (IManager)instance;

                    manager.Init();
                    managers.Add(manager);
                }
            }
        }

        protected override void Init()
        {
            AddManagers();
        }
    }
}
