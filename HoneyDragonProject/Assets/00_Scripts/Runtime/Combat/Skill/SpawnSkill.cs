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
        public SpawnSkill(SkillData data) : base(data) 
        {
            spawnObjectPrefabPath = data.PrefabPath;
            spawnObjects = CreatePool();
        }
        protected string spawnObjectPrefabPath;
        protected ObjectPooler<SpawnObject> spawnObjects;

        public virtual ObjectPooler<SpawnObject> CreatePool()
        {
         
            return new ObjectPooler<SpawnObject>(Utility.ResourceLoad<SpawnObject>(spawnObjectPrefabPath));
        }
    }
}
