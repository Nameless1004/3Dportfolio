using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.Vairable
{
    public class SharedVector3
    {

        public SharedVector3(Vector3 v) => Value = v;

        public static implicit operator Vector3(SharedVector3 sharedVector3)
        {
            return sharedVector3.Value;
        }

        public static implicit operator SharedVector3(Vector3 val)
        {
            return new SharedVector3(val);
        }
        public Vector3 Value { get; set; }
    }
}
