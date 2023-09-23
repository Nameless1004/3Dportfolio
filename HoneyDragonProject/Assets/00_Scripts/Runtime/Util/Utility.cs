using System.Collections;
using UnityEngine;

namespace RPG.Util
{
    public static class Utility
    {
        public static T ResourceLoad<T>(string path) where T : UnityEngine.Object   
        {
            T loaded = Resources.Load<T>(path);
            Debug.Assert(loaded is not null, $"Invalid Prefab Path: {path}");
            return loaded;
        }

    }
}
