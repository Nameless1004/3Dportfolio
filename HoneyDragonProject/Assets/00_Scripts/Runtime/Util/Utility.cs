using RPG.Core;
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
