using System;
using UnityEngine;

namespace RPG.Core
{
    [Serializable]
    public class EnemyData : Data
    {
        public string Name;
        public float MaxHp;
        public float MoveSpeed;
        public float AttackRange;
        public int AttackPower;

        public override void Save()
        {
            
        }

        public override void Load()
        {

        }
    }
}
