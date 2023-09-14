using System;
using UnityEngine;
using UnityEngine.Pool;

namespace RPG.Combat.Projectile
{
    public class ProjectilePooler
    {
        public ProjectilePooler(ProjectileBase prefab) 
        {
            this.prefab = prefab;
            Init();
        }

        ProjectileBase prefab;
        public ObjectPool<ProjectileBase> Pool { get; private set; }

        private void Init()
        {
            Pool = new ObjectPool<ProjectileBase>(CreateFunc, GetAction, ReleaseAction, DestroyAction);
        }

        public ProjectileBase CreateFunc()
        {
            var cl = MonoBehaviour.Instantiate(prefab);
            cl.SetPool(Pool);
            return cl; 
        }

        public void GetAction(ProjectileBase get) { }
        public void ReleaseAction(ProjectileBase release) { }
        public void DestroyAction(ProjectileBase destroy) { }
    }
}
