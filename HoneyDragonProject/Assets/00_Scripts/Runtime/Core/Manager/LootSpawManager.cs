using RPG.Core.Item;
using RPG.Util;
using UnityEngine;

namespace RPG.Core.Manager
{
    public class LootSpawManager : IManager
    {
        public enum LootType
        {
            Exp,
            Gold
        }

        ObjectPooler<Loot> Exp { get; set; }
        // ObjectPooler<Loot> Gold { get; set; }

        // 어떤 아이템을 드랍할지
        public void Spawn(LootType type, Vector3 position)
        {
            switch (type)
            {
                case LootType.Exp:
                    Exp.Get().Spawn(position);
                    break;
                case LootType.Gold:
                    // TODO: 추후 구현
                    break;
            }
        }

        public void Init()
        {
            Exp = new ObjectPooler<Loot>(ResourceCache.Load<Loot>("Prefab/Loot/Exp"));
        }
    }
}
