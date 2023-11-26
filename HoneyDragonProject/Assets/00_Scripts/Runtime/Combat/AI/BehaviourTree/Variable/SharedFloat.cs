using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree.Vairable
{

    public class SharedFloat
    {
        public SharedFloat(float v) => Value = v;

        public static implicit operator float(SharedFloat sharedFloat)
        {
            return sharedFloat.Value;
        }

        public static implicit operator SharedFloat(float val)
        {
            return new SharedFloat(val);
        }

        public float Value { get; set; }
    }
}
