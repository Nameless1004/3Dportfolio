using   UnityEngine;
using UnityEngine.Pool;

namespace RPG.Core.Item
{
    public abstract class Loot : MonoBehaviour, IPoolable<Loot>
    {
        public int Amount;
        protected bool isReleased = false;
        protected const float LOOT_LIFE_TIME = 30f;
        public abstract void Get(Player player);

        protected ObjectPool<Loot> owner;


        // Pooling ฐüทร
        public abstract void Spawn(Vector3 position);

        public abstract void OnDestroyAction();
        public abstract void OnGetAction();
        public abstract void OnReleaseAction();
        public abstract void SetPool(ObjectPool<Loot> owner);
    }
}
