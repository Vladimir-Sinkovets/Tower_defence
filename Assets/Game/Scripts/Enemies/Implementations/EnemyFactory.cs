using Assets.Game.Scripts.Enemies.Interfaces;
using Zenject;

namespace Assets.Game.Scripts.Enemies.Implementations
{
    public class EnemyFactory : IEnemyFactory 
    {
        private readonly IInstantiator _instantiator;

        public EnemyFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public Enemy Create(EnemyConfig config)
        {
            var enemy = _instantiator.InstantiatePrefabForComponent<Enemy>(config.Prefab);

            enemy.Init(config);

            return enemy;
        }
    }
}