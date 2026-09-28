using Assets.Game.Scripts.Battle.Configs;
using Assets.Game.Scripts.Battle.Ecs.Movement;
using Assets.Game.Scripts.Battle.Ecs.Unity;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Battle.Services.UnitFactories
{
    public class UnitFactory : IUnitFactory
    {
        private readonly IInstantiator _instantiator;

        public UnitFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public GameObject CreateUnit(UnitConfig config, Vector3 position, Entity entity, World world)
        {
            var unit = _instantiator.InstantiatePrefab(config.Prefab);
            
            unit.transform.position = position;
            
            unit.GetComponent<MonoEntity>()?.Bind(entity, world);
            
            world.GetStash<Position>().Set(entity, new() { Value = unit.transform.position });
            world.GetStash<Rotation>().Set(entity, new() { Value = unit.transform.rotation });

            foreach (var componentConfig in config.Components)
            {
                componentConfig.Apply(entity, world);
            }
            
            return unit;
        }
    }
}