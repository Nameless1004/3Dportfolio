using RPG.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Util
{
    public static class ResourceCache
    {
        private static readonly Dictionary<string, UnityEngine.Object> dict = new Dictionary<string, UnityEngine.Object>();

        public static T Load<T>(string path) where T : UnityEngine.Object
        {
            if(dict.TryGetValue(path, out var obj) == true)
            {
                return dict[path] as T;
            }

            T loaded = Resources.Load<T>(path);
            Debug.Assert(loaded is not null, $"Invalid Prefab Path: {path}");
            dict.Add(path, loaded);
            return loaded;
        }
    }
    public static class Utility
    {

        public static Transform FindNearestObject(Transform initiator, float range, int layerMask)
        {
            var collided = Physics.OverlapSphere(initiator.position, range, layerMask);
            Collider nearest = null;
            if (collided.Length > 0)
            {
                nearest = collided[0];
                float minDist = float.MaxValue;

                foreach (var enemy in collided)
                {
                    float dist = (initiator.position - enemy.transform.position).sqrMagnitude;
                    if (minDist > dist)
                    {
                        nearest = enemy;
                        minDist = dist;
                    }
                }
                return nearest.transform;
            }
            else
            {
                return null;
            }
        }
    }
}
