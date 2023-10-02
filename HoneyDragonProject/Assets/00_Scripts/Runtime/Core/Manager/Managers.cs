using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core.Manager
{
    public interface IManager
    {
        public void Init();
        public void Clear();
    }

    public class Managers : Singleton<Managers>
    {
        public DataManager Data { get; private set; }

        // PoolTransform 확인용
        [field:SerializeField]
        public GameManager Game { get; private set; }

        public SkillManager Skill { get; private set; }
        public StageManager Stage { get; private set; }
        public LootSpawManager Loot { get; private set; }

        private List<IManager> managers = new List<IManager>();

        public void Release()
        {
            managers.Clear();
        }

        public void Clear()
        {
            managers.ForEach(manager => manager.Clear());
        }
        private void AddManagers()
        {
            var properties = GetType().GetProperties();

            foreach(var property in properties)
            {
                Type type = property.PropertyType;
                Type managerType = type.GetInterface("IManager");

                if (managerType is not null)
                {
                    Debug.Log(type.Name);

                    var instance = Activator.CreateInstance(type);
                    property.SetValue(this, instance);

                    IManager manager = (IManager)instance;
                    manager.Init();
                    managers.Add(manager);
                }
            }
        }

        protected override void Init()
        {
            if (initialized == true) return;
            initialized = true;
            AddManagers();
        }
    }
}
