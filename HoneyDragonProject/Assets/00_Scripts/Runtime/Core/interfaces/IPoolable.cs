﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Pool;

namespace RPG.Core
{
    public interface IPoolable<T> where T : UnityEngine.Object, IPoolable<T>
    {
        public void SetPool(ObjectPooler<T> pool);
        public T GetPooledObject();
        public void OnGetAction();
        public void OnReleaseAction();
        public void OnDestroyAction();
    }
}
