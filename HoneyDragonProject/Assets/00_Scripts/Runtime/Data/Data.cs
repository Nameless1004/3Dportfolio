using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    [Serializable]
    public abstract class Data : ISaveLoadable
    {
        public abstract void Save();
        public abstract void Load();
    }
}
