using System.Threading;
using System.Threading.Tasks;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.Upgrades.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services.CastleFactories
{
    public class CastleFactory
    {
        private const string CastleRootGameObjectName = "Root";
        
        private readonly BuildingsConfig _buildingsConfig;
        private readonly IBuildingFactory _buildingFactory;
        private readonly IBuildingUpgradeApplier _buildingUpgradeApplier;
        private readonly IInstantiator _instantiator;
        private readonly GameSettings _settings;

        public CastleFactory(
            BuildingsConfig buildingsConfig,
            IBuildingFactory buildingFactory,
            IBuildingUpgradeApplier buildingUpgradeApplier,
            IInstantiator instantiator,
            GameSettingsService gameSettingsService)
        {
            _buildingsConfig = buildingsConfig;
            _buildingFactory = buildingFactory;
            _buildingUpgradeApplier = buildingUpgradeApplier;
            _instantiator = instantiator;
            _settings = gameSettingsService.Settings;
        }

        public async UniTask<(Health, Transform)> CreateCastleAsync(CancellationToken ct)
        {
            var root = new GameObject(CastleRootGameObjectName);
            var disposeHandler = root.AddComponent<DisposeOnDestroy>();

            var castleHealth = CreateHealth();
            
            RegisterDisposables(castleHealth, root, disposeHandler);

            var building = await CreateBuildingAsync(root);

            await building.AppearanceAnimation.PlayAsync(ct);
            
            return (castleHealth, root.transform);
        }

        private async UniTask<Building> CreateBuildingAsync(GameObject root)
        {
            var building = await _buildingFactory.CreateAsync(_buildingsConfig.CastleBuilding, _settings.CastleSettings.CastleBuilding, BuildingType.Castle);

            building.transform.SetParent(root.transform);
            building.transform.position = root.transform.position;
            return building;
        }

        private void RegisterDisposables(Health castleHealth, GameObject root, DisposeOnDestroy disposeHandler)
        {
            var shaker = _instantiator.Instantiate<DamageShaker>(new object[] { castleHealth, root.transform });
            var handler = _instantiator.Instantiate<AnalyticsCastleDamageHandler>(new object[] { castleHealth });
            
            handler.Init();
            
            disposeHandler.Add(shaker, handler);
        }

        private Health CreateHealth()
        {
            var castleHp = _buildingUpgradeApplier.ApplyCastleHpUpgrade(_settings.CastleSettings.CastleHp);
            var castleHealth = new Health(castleHp);
            
            return castleHealth;
        }
    }
}