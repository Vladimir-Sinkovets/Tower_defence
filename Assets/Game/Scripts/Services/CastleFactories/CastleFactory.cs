using System.Threading;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Buildings.Interfaces;
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

        private CancellationTokenSource _startGameCts;

        public CastleFactory(
            BuildingsConfig buildingsConfig,
            IBuildingFactory buildingFactory,
            IBuildingUpgradeApplier buildingUpgradeApplier,
            IInstantiator instantiator)
        {
            _buildingsConfig = buildingsConfig;
            _buildingFactory = buildingFactory;
            _buildingUpgradeApplier = buildingUpgradeApplier;
            _instantiator = instantiator;
        }

        public async UniTask<Health> CreateCastle(CancellationToken ct)
        {
            var root = new GameObject(CastleRootGameObjectName);
            var disposeHandler = root.AddComponent<DisposeOnDestroy>();

            var castleHealth = CreateHealth(root);
            
            RegisterDisposables(castleHealth, root, disposeHandler);

            var building = CreateBuilding(root);

            await building.AppearanceAnimation.Play(ct);
            
            return castleHealth;
        }

        private Building CreateBuilding(GameObject root)
        {
            var building = _buildingFactory.Create(_buildingsConfig.CastleBuilding, BuildingType.Castle);

            building.transform.SetParent(root.transform);
            building.transform.position = root.transform.position;
            return building;
        }

        private void RegisterDisposables(Health castleHealth, GameObject root, DisposeOnDestroy disposeHandler)
        {
            var shaker = _instantiator.Instantiate<DamageShaker>(new object[] { castleHealth, root.transform });
            var handler = _instantiator.Instantiate<AnalyticsCastleDamageHandler>(new object[] { castleHealth });
            
            disposeHandler.Add(shaker, handler);
        }

        private Health CreateHealth(GameObject root)
        {
            var castleHp = _buildingUpgradeApplier.ApplyCastleHpUpgrade(_buildingsConfig.CastleHp);
            var castleHealth = new Health(castleHp, root.transform);
            return castleHealth;
        }
    }
}