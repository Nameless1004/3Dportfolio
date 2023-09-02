using System;
using System.IO;
using System.Reflection.Emit;
using UnityEngine;

namespace RPG.Core
{
    public class DataManager : MonoBehaviour
    {
        public enum DataType
        {
            Player,
            Enemy,
            Skill
        }
        static private DataManager instance;
        static public DataManager Instance => instance;

        private void Awake()
        {
            instance = this;
        }

    }
}
