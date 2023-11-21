using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat.AI.BehaviourTree
{
    public class Blackboard
    {
        public object this[string key]
        {
            get
            {
                return this.GetData(key);
            }
            set
            {
                this.SetData(key, value);
            }
        }

        Dictionary<string, object> dataContext = new Dictionary<string, object>();
        public Dictionary<string, object> GetDataContext() => dataContext;

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
            if(dataContext.TryGetValue(key, out var value) == false)
            {
                // 밸류가 없으면 잘못된 접근
                if (value == null)
                {
                    var ex = new Exception($"블랙보드에 {key}값이 없습니다.");
                    throw ex;
                }
            }

            return value;
        }

        public T GetData<T>(string key)
        {
            if (dataContext.TryGetValue(key, out var value) == false)
            {
                // 밸류가 없으면 잘못된 접근
                if(value == null)
                {
                    var ex = new Exception($"블랙보드에 {key}값이 없습니다.");
                    throw ex;
                }
                if(value.GetType() == typeof(T))
                {
                    var ex = new Exception($"value의 타입과 T의 타입이 맞지않음 value: {value.GetType().Name} / T: {typeof(T).Name}");
                    throw ex;
                }
            }

            return (T)value;
        }

    }
}
