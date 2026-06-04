using System.Threading;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.Upgrades;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Services.CastleFactories
{
    public class CastleFactory
    {
        private readonly BuildingsConfig _buildingsConfig;
        private readonly IBuildingFactory _buildingFactory;
        private readonly IBuildingUpgradeApplier _buildingUpgradeApplier;

        private CancellationTokenSource _startGameCts;

        public CastleFactory(BuildingsConfig buildingsConfig, IBuildingFactory buildingFactory, IBuildingUpgradeApplier buildingUpgradeApplier)
        {
            _buildingsConfig = buildingsConfig;
            _buildingFactory = buildingFactory;
            _buildingUpgradeApplier = buildingUpgradeApplier;
        }

        public async UniTask<Health> CreateCastle(CancellationToken ct)
        {
            var castleHealth = Object.Instantiate(_buildingsConfig.CastleHealthPrefab);

            var castleHp = _buildingUpgradeApplier.ApplyCastleHpUpgrade(_buildingsConfig.CastleHp);
            
            castleHealth.Init(castleHp);
            
            if (castleHealth.TryGetComponent<DamageShaker>(out var shaker))
                shaker.Init(castleHealth, castleHealth.transform);
            
            if (castleHealth.TryGetComponent<AnalyticsCastleDamageHandler>(out var handler))
                handler.Init(castleHealth);
            
            
            var building = _buildingFactory.Create(_buildingsConfig.CastleBuilding, BuildingType.Castle);

            building.transform.parent = castleHealth.transform;
            building.transform.position = castleHealth.transform.position;

            await building.AppearanceAnimation.Play(ct);
            
            return castleHealth;
        }
    }
}