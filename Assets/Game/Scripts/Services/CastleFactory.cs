using System.Threading;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Services
{
    public class CastleFactory
    {
        private readonly BuildingsConfig _buildingsConfig;
        private readonly IBuildingFactory _buildingFactory;

        private CancellationTokenSource _startGameCts;

        public CastleFactory(BuildingsConfig buildingsConfig, IBuildingFactory buildingFactory)
        {
            _buildingsConfig = buildingsConfig;
            _buildingFactory = buildingFactory;
        }

        public async UniTask<Health> CreateCastle(CancellationToken ct)
        {
            var castleHealth = Object.Instantiate(_buildingsConfig.CastleHealthPrefab);
            
            castleHealth.Init(_buildingsConfig.CastleHp);
            
            if (castleHealth.TryGetComponent<DamageShaker>(out var shaker))
                shaker.Init(castleHealth, castleHealth.transform);
            
            if (castleHealth.TryGetComponent<AnalyticsCastleDamageHandler>(out var handler))
                handler.Init(castleHealth);
            
            
            var building = _buildingFactory.Create(_buildingsConfig.CastleBuilding);

            building.transform.parent = castleHealth.transform;
            building.transform.position = castleHealth.transform.position;

            await building.AppearanceAnimation.Play(ct);
            
            return castleHealth;
        }
    }
}