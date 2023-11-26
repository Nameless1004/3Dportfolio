using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.Vairable
{
    public class SharedInt
    {
        public SharedInt(int v) => Value = v;

        public static implicit operator int(SharedInt sharedInt)
        {
            return sharedInt.Value;
        }

        public static implicit operator SharedInt(int val)
        {
            return new SharedInt(val);
        }

        public int Value { get; set; }
    }
}
