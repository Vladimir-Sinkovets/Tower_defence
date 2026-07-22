using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Services.AssetProviders;
using Assets.Game.Scripts.Services.Configs.Buildings;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings.Implementations
{
    public class BuildingFactory : IBuildingFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public BuildingFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public async UniTask<Building> CreateAsync(BuildingConfig config, BuildingSettings settings, BuildingType buildingType)
        {
            var prefab = await _assetProvider.Load<GameObject>(config.Prefab);
            
            var building = _instantiator.InstantiatePrefabForComponent<Building>(prefab);

            building.Init(config, settings, buildingType);

            return building;
        }
    }
}