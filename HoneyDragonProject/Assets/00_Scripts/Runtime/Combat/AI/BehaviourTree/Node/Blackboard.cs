using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AI.BehaviourTree
{
    public class Blackboard
    {
        Dictionary<string, object> dataContext = new Dictionary<string, object>();

        public void SetData(string key, object value)
        {
            if(dataContext.TryGetValue(key, out var data) == false)
            {
                dataContext.Add(key, value);
            }
            else
            {
                dataContext[key] = value;
            }
        }

        public object GetData(string key) 
        {
            if (dataContext.TryGetValue(key, out var value) == false || value == null)
            {
                return null;
            }

            return value;
        }

    }
}
