using RPG.Combat.Projectile;
using RPG.Core;
using RPG.Core.Data;
using System.Net.Mime;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public abstract class ShootingSkill : ActiveSkill
    {
        public ShootingSkill(SkillData data) : base(data) 
        {
            projectilePrefabPath = data.ProjectilePath;
            projectiles = CreatePool();
        }

        protected string projectilePrefabPath;
        protected ObjectPooler<ProjectileBase> projectiles;

        
        public abstract ObjectPooler<ProjectileBase> CreatePool();

        public Transform FindNearestEnemy(Creature initiator, int layerMask)
        {
            var collided = Physics.OverlapSphere(initiator.position, 10f, layerMask);
            Collider nearest = null;
            if (collided.Length > 0)
            {
                nearest = collided[0];
                float minDist = float.MaxValue;

                foreach (var enemy in collided)
                {
                    float dist = (initiator.position - enemy.transform.position).sqrMagnitude;
                    if (minDist > dist)
                    {
                        nearest = enemy;
                        minDist = dist;
                    }
                }
                return nearest.transform;
            }
            else
            {
                return null;
            }
        }
    }
}
