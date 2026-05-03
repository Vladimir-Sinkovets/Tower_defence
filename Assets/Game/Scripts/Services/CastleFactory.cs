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
        private const string CastleRootObjectName = "Castle"; 
        
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
            var castle = new GameObject(CastleRootObjectName);
            
            var castleHealth = castle.AddComponent<Health>();
            castleHealth.Init(_buildingsConfig.CastleHp);
            
            var shaker = castle.AddComponent<DamageShaker>();
            shaker.Init(castleHealth);
            
            
            var building = _buildingsConfig.CastleBuilding.Create(_instantiator);

            building.transform.parent = castle.transform;
            building.transform.position = castle.transform.position;

            await building.AppearanceAnimation.Play(ct);
            
            return castleHealth;
        }
    }
}