using RPG.Combat.AI.BehaviourTree.Vairable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree.Variable
{
    public class SharedBool 
    {
        public SharedBool(bool v) => Value = v;

        public static implicit operator bool(SharedBool sharedBool)
        {
            return sharedBool.Value;
        }

        public static implicit operator SharedBool(bool val)
        {
            return new SharedBool(val);
        }

        public bool Value { get; set; }
    }
}
