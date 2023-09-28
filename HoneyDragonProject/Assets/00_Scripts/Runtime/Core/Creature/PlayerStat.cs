using RPG.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Core
{
    [Serializable]
    public class PlayerStat
    {
        public int Level;
        public int Exp;
        public float MoveSpeed;
        public float AttackPower;
        public float Defense;

        public void SetData(PlayerData data)
        {
            this.Level = data.Level;
            this.Exp = data.Exp;
            this.MoveSpeed = data.MoveSpeed;
            this.AttackPower = data.AttackPower;
            this.Defense = data.Defense;
        }
    }
}
