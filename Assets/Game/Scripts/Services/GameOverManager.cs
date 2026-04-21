using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies;
using System;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class GameOverManager
    {
        public event Action<GameOverResult> OnGameOver;
        
        private Registry<Enemy> _enemyRegistry;
        private Registry<Building> _buildingRegistry;
        private GameStatistics _gameStatistics;
        private CurrencyBank _currencyBank;
        private MetaCurrencyConfig _metaCurrencyConfig;
        private MetaCurrencyService _metaCurrencyService;
        private WavesController _wavesController;

        [Inject]
        public void Construct(
            WavesController waveController,
            Registry<Building> buildingRegistry,
            Registry<Enemy> enemyRegistry,
            GameStatistics gameStatistics,
            CurrencyBank currencyBank,
            MetaCurrencyConfig metaCurrencyConfig,
            MetaCurrencyService metaCurrencyService)
        {
            _wavesController = waveController;
            _enemyRegistry = enemyRegistry;
            _buildingRegistry = buildingRegistry;
            _gameStatistics = gameStatistics;
            _currencyBank = currencyBank;
            _metaCurrencyConfig = metaCurrencyConfig;
            _metaCurrencyService = metaCurrencyService;
        }

        public void GameOver()
        {
            StopEnemies();

            StopBuildings();

            var earnedMetaCurrency = CalculateMetaData();

            ApplyMetaData(earnedMetaCurrency);
            
            OnGameOver?.Invoke(new GameOverResult()
            {
                Waves = _wavesController.WavesCount,
                Kills = _gameStatistics.KilledEnemiesCount,
                MetaCurrency = _metaCurrencyService.Total,
                Currency = _currencyBank.Total,
            });
        }

        private void ApplyMetaData(int value) => _metaCurrencyService.Add(value);

        private int CalculateMetaData() => _wavesController.WavesCount * _metaCurrencyConfig.MetaCurrencyPerWave +
                _gameStatistics.KilledEnemiesCount * _metaCurrencyConfig.MetaCurrencyPerKill;

        private void StopBuildings()
        {
            foreach (var building in _buildingRegistry.All)
            {
                building.Stop();
            }
        }

        private void StopEnemies()
        {
            foreach (var enemy in _enemyRegistry.All)
            {
                enemy.Deactivate();
            }
        }
    }
    
    public class GameOverResult
    {
        public int Waves;
        public int Kills;
        public int Currency;
        public int MetaCurrency;
    }
}