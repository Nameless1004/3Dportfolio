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
                    var expAmount = CalculateExpAmount();
                    Exp.Get().Spawn(position, expAmount.Item1, expAmount.Item2);
                    break;
                case LootType.Gold:
                    // TODO: 추후 구현
                    break;
            }
        }

        Player currentPlayer;
        // item1: min, item2: max
        private (int, int) CalculateExpAmount()
        {
            if(currentPlayer == null)
            {
                currentPlayer = Managers.Instance.Game.CurrentPlayer;
            }

            int defaultAmount = 5;
            int min = (int)((currentPlayer.Status.Level * defaultAmount) * 0.5f);
            int max = currentPlayer.Status.Level * defaultAmount;
            return (min, max);
        }

        public void Init()
        {
            Exp = new ObjectPooler<Loot>(ResourceCache.Load<Loot>("Prefab/Loot/Exp"));
        }

        public void Clear()
        {

        }
    }
}
