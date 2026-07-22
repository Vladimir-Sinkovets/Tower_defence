using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.AssetProviders;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Enemies.Implementations
{
    public class EnemyFactory : IEnemyFactory 
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public EnemyFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }
        
        public async UniTask<Enemy> CreateAsync(EnemyConfig config)
        {
            var prefab = await _assetProvider.Load<GameObject>(config.Prefab);
            
            var enemy = _instantiator.InstantiatePrefabForComponent<Enemy>(prefab.GetComponent<Enemy>());

            return enemy;
        }
    }
}