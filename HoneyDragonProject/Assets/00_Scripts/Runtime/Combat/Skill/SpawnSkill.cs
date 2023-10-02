using Cysharp.Threading.Tasks;
using RPG.Combat.Projectile;
using RPG.Combat.Skill;
using RPG.Core;
using RPG.Core.Data;
using RPG.Util;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public abstract class SpawnSkill : ActiveSkill
    {
        public override void SetData(SkillData data)
        {
            base.SetData(data);
            spawnCount = data.SpawnCount;
            spawnObjects = CreatePool();
        }

        [SerializeField]
        protected SpawnObject spawnObjectPrefab;
        protected ObjectPooler<SpawnObject> spawnObjects;
        protected int spawnCount;

        public virtual ObjectPooler<SpawnObject> CreatePool()
        {
         
            return new ObjectPooler<SpawnObject>(spawnObjectPrefab);
        }
    }
}
