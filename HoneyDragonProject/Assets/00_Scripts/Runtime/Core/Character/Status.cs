using System;
using UnityEngine;

namespace RPG.Core.Character
{
    [Serializable]
    public class Status
    {
        public int Level;
        public int Hp;
        public int AttackPower;
    }

    [Serializable]
    public class EnemyStatus : Status
    {

    }
}
