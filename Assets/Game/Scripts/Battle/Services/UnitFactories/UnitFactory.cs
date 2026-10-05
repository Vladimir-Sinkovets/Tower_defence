using Assets.Game.Scripts.Battle.Common;
using Assets.Game.Scripts.Battle.Configs;
using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Ecs.Attacks;
using Assets.Game.Scripts.Battle.Ecs.Movement;
using Assets.Game.Scripts.Battle.Ecs.Spawn;
using Assets.Game.Scripts.Battle.Ecs.Unity;
using Assets.Game.Scripts.Battle.Services.UpgradeAppliers;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Battle.Services.UnitFactories
{
    public class UnitFactory : IUnitFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IUpgradeApplier _upgradeApplier;

        public UnitFactory(IInstantiator instantiator, IUpgradeApplier upgradeApplier)
        {
            _instantiator = instantiator;
            _upgradeApplier = upgradeApplier;
        }

        public GameObject CreateUnit(UnitConfig config, Vector3 position, Entity entity, World world, int teamId)
        {
            var unit = _instantiator.InstantiatePrefab(config.Prefab);
            
            unit.transform.position = position;
            
            unit.GetComponent<MonoEntity>()?.Bind(entity, world);
            
            world.GetStash<Position>().Set(entity, new() { Value = unit.transform.position });
            world.GetStash<Rotation>().Set(entity, new() { Value = unit.transform.rotation });
            world.GetStash<Team>().Set(entity, new() {  Index = teamId });

            foreach (var componentConfig in config.Components)
            {
                componentConfig.Apply(entity, world);
            }

            if (teamId == TeamIndexes.Player)
            {
                ref var attacker = ref world.GetStash<Attacker>().Get(entity);

                if (world.GetStash<Tank>().Has(entity))
                    attacker.Damage = _upgradeApplier.ApplyTankDamageUpgrade(attacker.Damage);
                else
                    attacker.Damage = _upgradeApplier.ApplyDefaultUnitDamageUpgrade(attacker.Damage);
            }
            
            
            return unit;
        }
    }
}