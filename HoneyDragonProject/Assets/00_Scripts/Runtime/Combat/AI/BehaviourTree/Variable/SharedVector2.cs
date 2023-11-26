using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace RPG.Combat.AI.BehaviourTree.Vairable
{
    public class SharedVector2
    {
        public SharedVector2(Vector2 v) => Value = v;

        public static implicit operator Vector2(SharedVector2 sharedVector2)
        {
            return sharedVector2.Value;
        }

        public static implicit operator SharedVector2(Vector2 val)
        {
            return new SharedVector2(val);
        }
        public Vector2 Value { get; set; }
    }
}
