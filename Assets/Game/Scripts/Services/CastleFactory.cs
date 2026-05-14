using System.Threading;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class CastleFactory
    {
        private readonly BuildingsConfig _buildingsConfig;
        private readonly IInstantiator _instantiator;
        
        private CancellationTokenSource _startGameCts;

        public CastleFactory(BuildingsConfig buildingsConfig, IInstantiator instantiator)
        {
            _buildingsConfig = buildingsConfig;
            _instantiator = instantiator;
        }

        public async UniTask<Health> CreateCastle(CancellationToken ct)
        {
            var castleHealth = Object.Instantiate(_buildingsConfig.CastleHealthPrefab);
            
            castleHealth.Init(_buildingsConfig.CastleHp);
            
            if (castleHealth.TryGetComponent<DamageShaker>(out var shaker))
                shaker.Init(castleHealth, castleHealth.transform);
            
            
            var building = _buildingsConfig.CastleBuilding.Create(_instantiator);

            building.transform.parent = castleHealth.transform;
            building.transform.position = castleHealth.transform.position;

            await building.AppearanceAnimation.Play(ct);
            
            return castleHealth;
        }
    }
}