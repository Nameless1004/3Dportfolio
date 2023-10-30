using RPG.Combat.Projectile;
using RPG.Util;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace RPG.Core
{
    public class ObjectPooler<T> where T : MonoBehaviour, IPoolable<T>
    {
        public ObjectPooler(T prefab, Transform parent = null)
        {
            this.Prefab = prefab;
            Pool = new ObjectPool<T>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy);
            Parent = parent;
        }


        public T Prefab { get; protected set; }
        public Transform Parent { get; protected set; }
        public ObjectPool<T> Pool { get; private set; }
        public int ActiveCount => Pool.CountActive;
        public int InActiveCount => Pool.CountInactive;
        public int CountAll => Pool.CountAll;

        public T CreateFunc()
        {
            T cl = MonoBehaviour.Instantiate<T>(Prefab, Parent);
            cl.SetPool(this.Pool);
            return cl;
        }

        public void ActionOnGet(T element)
        {
            element.OnGetAction();
        }
        public  void ActionOnRelease(T element) => element.OnReleaseAction();
        public  void ActionOnDestroy(T element) => element.OnDestroyAction();

        /// <summary>
        /// 안전하게 가져오기 위해 사용
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Get성공 시 : True, 실패 시 : false</returns>
        public bool TryGet(out T obj)
        {
            obj = Pool.Get();

            if (obj == null)
                return false;

            return true;
        }

        public T Get() => Pool.Get();
        public U Get<U>() where U : T => (U)Pool.Get();
        public void Release(T element) => Pool.Release(element);
        public void Clear() => Pool.Clear();
        
    }
}
