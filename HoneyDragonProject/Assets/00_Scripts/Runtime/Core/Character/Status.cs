using System;
using UnityEngine;

namespace RPG.Core.Character
{
    [Serializable]
    public class Status
    {
        public string Tag;
        public int Id;
        public int Level;
        public int Hp;
        public int AttackPower;


        private void Clone(Status stat)
        {
            Level = stat.Level;
            Hp = stat.Hp;
            AttackPower = stat.AttackPower;
        }

        private void LoadStatus(string key, int id, int level)
        {
            Clone(JsonUtility.FromJson<Status>($"{key}_{id}_{level}"));
        }
    }
}
