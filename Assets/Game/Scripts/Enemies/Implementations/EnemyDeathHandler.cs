using System;
using Assets.Game.Scripts.Services.CurrencyBanks;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Services.Statistics;
using Zenject;

namespace Assets.Game.Scripts.Enemies.Implementations
{
    public class EnemyDeathHandler : IInitializable, IDisposable
    {
        private readonly Registry<Enemy> _enemies;
        private readonly ICurrencyBank _currencyBank;
        private readonly IGameStatistics _statistics;

        public EnemyDeathHandler(Registry<Enemy> enemies, ICurrencyBank currencyBank, IGameStatistics statistics)
        {
            _enemies = enemies;
            _currencyBank = currencyBank;
            _statistics = statistics;
        }
        
        public void Initialize()
        {
            _enemies.OnRegistered += OnEnemyRegisteredHandler;
            _enemies.OnUnregistered += OnEnemyUnregisteredHandler;
        }

        private void OnEnemyRegisteredHandler(Enemy enemy) => enemy.OnDied += OnDiedHandler;

        private void OnEnemyUnregisteredHandler(Enemy enemy) => enemy.OnDied -= OnDiedHandler;

        private void OnDiedHandler(Enemy enemy)
        {
            _currencyBank.Add(enemy.Award);
            
            _statistics.IncreaseKilledEnemyCount();
        }

        public void Dispose()
        {
            _enemies.OnRegistered -= OnEnemyRegisteredHandler;
            _enemies.OnUnregistered -= OnEnemyUnregisteredHandler;
        }
    }
}