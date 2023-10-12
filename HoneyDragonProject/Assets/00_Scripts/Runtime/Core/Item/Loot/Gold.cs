using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace RPG.Core.Item
{
    public class Gold : Loot
    {
        public override void Get(Player player)
        {
            player.GetGold(maxAmount);
        }


        public override void OnDestroyAction()
        {
            throw new NotImplementedException();
        }

        public override void OnGetAction()
        {
            throw new NotImplementedException();
        }

        public override void OnReleaseAction()
        {
            throw new NotImplementedException();
        }

        public override void SetPool(ObjectPool<Loot> owner)
        {
            throw new NotImplementedException();
        }

        public override void Spawn(Vector3 position, int minAmount, int maxAmount)
        {
            throw new NotImplementedException();
        }
    }
}
