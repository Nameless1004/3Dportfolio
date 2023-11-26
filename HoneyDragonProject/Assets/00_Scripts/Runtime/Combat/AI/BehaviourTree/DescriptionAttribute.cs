using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Combat.AI.BehaviourTree
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class DescriptionAttribute : Attribute
    {
        public readonly string Description;
        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
