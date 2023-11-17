using   UnityEngine;
using UnityEngine.Pool;

namespace RPG.Core.Item
{
    public abstract class Loot : MonoBehaviour, IPoolable<Loot>
    {
        protected int minAmount;
        protected int maxAmount;
        public int Amount => Random.Range(minAmount, maxAmount + 1);
        protected bool isReleased = false;
        protected const float LOOT_LIFE_TIME = 30f;
        public abstract void Get(Creature player);

        protected ObjectPool<Loot> owner;



        // Pooling ฐüทร
        public abstract void Spawn(Vector3 position, int minAmount, int maxAmount);

        public abstract void OnDestroyAction();
        public abstract void OnGetAction();
        public abstract void OnReleaseAction();
        public abstract void SetPool(ObjectPool<Loot> owner);
    }
}
