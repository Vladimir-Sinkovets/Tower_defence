using Assets.Game.Scripts.Battle.Ecs.Damage;
using Assets.Game.Scripts.Battle.Ecs.HealthFeature;
using Assets.Game.Scripts.Battle.Ecs.Movement;
using Assets.Game.Scripts.Battle.Ecs.Unity;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Battle.Services.UnitFactories
{
    public class UnitFactory : IUnitFactory
    {
        private readonly UnitsConfig _unitsConfig;
        private readonly IInstantiator _instantiator;

        public UnitFactory(UnitsConfig unitsConfig, IInstantiator instantiator)
        {
            _unitsConfig = unitsConfig;
            _instantiator = instantiator;
        }
        
        public GameObject CreateUnit(Vector3 position, Entity entity, World world)
        {
            var unit = _instantiator.InstantiatePrefab(_unitsConfig.Prefab);
            
            unit.transform.position = position;
            
            unit.GetComponent<MonoEntity>()?.Bind(entity, world);
            
            world.GetStash<Position>().Set(entity, new() { Value = unit.transform.position });
            world.GetStash<Rotation>().Set(entity, new() { Value = unit.transform.rotation });
            world.GetStash<Attacker>().Set(entity, new() { Damage = 1});
            world.GetStash<Health>().Set(entity, new() { Hp = 2 });
            
            return unit;
        }
    }
}