using Assets.Game.Scripts.Buildings.Interfaces;
using Zenject;

namespace Assets.Game.Scripts.Buildings.Implementations
{
    public class BuildingFactory : IBuildingFactory
    {
        private readonly IInstantiator _instantiator;

        public BuildingFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public Building Create(BuildingConfig config, BuildingType buildingType)
        {
            var building = _instantiator.InstantiatePrefabForComponent<ShootingBuilding>(config.Prefab);

            building.Init(config, buildingType);

            return building;
        }
    }
}