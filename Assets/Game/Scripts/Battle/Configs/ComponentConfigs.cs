using System;
using Assets.Game.Scripts.Battle.Ecs.Attacks;
using Assets.Game.Scripts.Battle.Ecs.HealthFeature;
using Assets.Game.Scripts.Battle.Ecs.Movement;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Configs
{
    public interface IComponentConfig
    {
        void Apply(Entity entity, World world);
    }

    [Serializable]
    public class HealthConfig : IComponentConfig
    {
        [SerializeField] private int _hp;
        
        public void Apply(Entity entity, World world) => 
            world.GetStash<Health>().Set(entity, new() { Hp = _hp });
    }

    [Serializable]
    public class AttackConfig : IComponentConfig
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _attackRange = 0.3f;
        [SerializeField] private float _timeBetweenAttacks = 1.5f;
        
        public void Apply(Entity entity, World world)
        {
            world.GetStash<Attacker>().Set(entity, new()
            {
                Damage = _damage,
                TimeBetweenAttacks = _timeBetweenAttacks,
            });
            world.GetStash<Attack>().Set(entity, new() { Timer = 0.0f });
            world.GetStash<AttackRange>().Set(entity, new()
            {
                Value = _attackRange,
            });
        }
    }

    [Serializable]
    public class MoveConfig : IComponentConfig
    {
        [SerializeField] private float _speed;
        
        public void Apply(Entity entity, World world) => 
            world.GetStash<Speed>().Set(entity, new() { Value = _speed });
    }
}