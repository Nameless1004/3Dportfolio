using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Core.Item
{
    public class Gold : Loot
    {
        public override void Get(Player player)
        {
            player.GetGold(Amount);
        }

        public override Loot GetPooledObject()
        {
            throw new NotImplementedException();
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

        public override void SetPool(ObjectPooler<Loot> pool)
        {
            throw new NotImplementedException();
        }

        public override void Spawn(Vector3 position)
        {
            throw new NotImplementedException();
        }
    }
}
