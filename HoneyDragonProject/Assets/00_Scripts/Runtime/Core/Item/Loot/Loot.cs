using   UnityEngine;

namespace RPG.Core.Item
{
    public abstract class Loot : MonoBehaviour, IPoolable<Loot>
    {
        public int Amount;
        public abstract void Get(Player player);

        protected ObjectPooler<Loot> pool;


        // Pooling ฐüทร
        public abstract void Spawn(Vector3 position);

        public abstract Loot GetPooledObject();
        public abstract void OnDestroyAction();
        public abstract void OnGetAction();
        public abstract void OnReleaseAction();
        public abstract void SetPool(ObjectPooler<Loot> pool);
    }
}
