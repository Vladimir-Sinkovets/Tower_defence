using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies;
using System;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI.Windows;

namespace Assets.Game.Scripts.Services
{
    public class GameOverManager : IDisposable
    {
        private readonly Registry<Enemy> _enemyRegistry;
        private readonly Registry<Building> _buildingRegistry;
        private readonly GameStatistics _gameStatistics;
        private readonly CurrencyBank _currencyBank;
        private readonly MetaCurrencyConfig _metaCurrencyConfig;
        private readonly MetaCurrencyService _metaCurrencyService;
        private readonly IWindowsManager _windowsManager;
        private readonly WavesController _wavesController;

        private Health _castleHealth;
        
        public GameOverResult GameOverResult { get; private set; }

        public GameOverManager(
            WavesController waveController,
            Registry<Building> buildingRegistry,
            Registry<Enemy> enemyRegistry,
            GameStatistics gameStatistics,
            CurrencyBank currencyBank,
            MetaCurrencyConfig metaCurrencyConfig,
            MetaCurrencyService metaCurrencyService,
            IWindowsManager windowsManager)
        {
            _wavesController = waveController;
            _enemyRegistry = enemyRegistry;
            _buildingRegistry = buildingRegistry;
            _gameStatistics = gameStatistics;
            _currencyBank = currencyBank;
            _metaCurrencyConfig = metaCurrencyConfig;
            _metaCurrencyService = metaCurrencyService;
            _windowsManager = windowsManager;
        }

        public void Init(Health castleHealth)
        {
            _castleHealth = castleHealth;
            castleHealth.OnDied += OnDiedHandler;
        }

        private void OnDiedHandler() => GameOver();

        private void GameOver()
        {
            StopEnemies();

            StopBuildings();

            StopWaves();

            var earnedMetaCurrency = CalculateMetaCurrency();

            ApplyMetaData(earnedMetaCurrency);
            
            GameOverResult = new GameOverResult()
            {
                Waves = _wavesController.WavesCount,
                Kills = _gameStatistics.KilledEnemiesCount,
                Currency = _currencyBank.Total,
                EarnedMetaCurrency = earnedMetaCurrency,
            };
            
            _windowsManager.Open(WindowType.EndGame);
        }

        private void StopWaves() => _wavesController.Stop();

        private void ApplyMetaData(int value) => _metaCurrencyService.Add(value);

        private int CalculateMetaCurrency() => _wavesController.WavesCount * _metaCurrencyConfig.MetaCurrencyPerWave +
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
        
        public void Dispose()
        {
            _castleHealth.OnDied -= OnDiedHandler;
        }
    }
    
    public class GameOverResult
    {
        public int Waves;
        public int Kills;
        public int Currency;
        public int EarnedMetaCurrency;
    }
}