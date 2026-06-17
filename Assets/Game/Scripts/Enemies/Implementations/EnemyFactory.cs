using System.Threading.Tasks;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.AssetProviders;
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
        
        public async Task<Enemy> Create(EnemyConfig config)
        {
            var prefab = await _assetProvider.Load<GameObject>(config.Prefab);
            
            var enemy = _instantiator.InstantiatePrefabForComponent<Enemy>(prefab.GetComponent<Enemy>());

            enemy.Init(config);

            return enemy;
        }
    }
}