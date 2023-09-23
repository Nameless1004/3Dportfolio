using Cysharp.Threading.Tasks;
using RPG.Core;
using RPG.Core.Data;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace RPG.Combat.Skill
{
    public interface Skill
    {
        public SkillData Data { get;}
    }

    public abstract class SkillBase : Skill
    {
        public SkillBase(SkillData data)
        {
            Data = data;
            Id = data.Id;
        }

        public int Id;
        public Sprite Icon;
        public int CurrentLevel;

        public SkillData Data { get; protected set; }
        

        public abstract void Levelup();
    }

    public abstract class ActiveSkill : SkillBase
    {
        public ActiveSkill(SkillData data) : base(data)
        {
        }


        public abstract UniTaskVoid Activate(Creature initiator, CancellationToken token = default);
        public float currentCoolTime = float.MaxValue;

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
