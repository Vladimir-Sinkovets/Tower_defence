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
        
        public GameObject CreateUnit(Vector3 position)
        {
            var unit = _instantiator.InstantiatePrefab(_unitsConfig.Prefab);
            
            unit.transform.position = position;
            
            return unit;
        }
    }
}