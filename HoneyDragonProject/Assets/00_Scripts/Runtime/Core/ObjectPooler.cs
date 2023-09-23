using RPG.Combat.Projectile;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace RPG.Core
{
    public class ObjectPooler<T> where T : Object, IPoolable<T>
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

        public void OnDestroy()
        {
            Pool.Clear();
            Pool.Dispose();
        }

        public T CreateFunc()
        {
            IPoolable<T> cl = MonoBehaviour.Instantiate(Prefab, Parent).GetComponent<IPoolable<T>>();
            cl.SetPool(this);
            return cl.GetPooledObject();
        }

        public  void ActionOnGet(T element) => element?.OnGetAction();
        public  void ActionOnRelease(T element) => element?.OnReleaseAction();
        public  void ActionOnDestroy(T element) => element?.OnDestroyAction();

        public T Get() => Pool.Get();
        public U Get<U>() where U : T => (U)Pool?.Get();
        public void Release(T element) => Pool?.Release(element);
        
    }
}
