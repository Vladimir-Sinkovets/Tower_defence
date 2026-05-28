using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies;
using System;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Saves;
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
        private readonly IWindowsManager _windowsManager;
        private readonly IGameplayAnalytics _gameplayAnalytics;
        private readonly IWavesController _wavesController;
        private readonly ISaveService _saveService;

        private Health _castleHealth;

        public GameOverResult GameOverResult { get; private set; }

        public GameOverManager(
            IWavesController waveController,
            Registry<Building> buildingRegistry,
            Registry<Enemy> enemyRegistry,
            GameStatistics gameStatistics,
            CurrencyBank currencyBank,
            MetaCurrencyConfig metaCurrencyConfig,
            ISaveService saveService,
            IWindowsManager windowsManager,
            IGameplayAnalytics gameplayAnalytics)
        {
            _wavesController = waveController;
            _enemyRegistry = enemyRegistry;
            _buildingRegistry = buildingRegistry;
            _gameStatistics = gameStatistics;
            _currencyBank = currencyBank;
            _metaCurrencyConfig = metaCurrencyConfig;
            _saveService = saveService;
            _windowsManager = windowsManager;
            _gameplayAnalytics = gameplayAnalytics;
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

            ApplySaveData(earnedMetaCurrency, _wavesController.WavesCount);
            
            GameOverResult = new GameOverResult()
            {
                Waves = _wavesController.WavesCount,
                Kills = _gameStatistics.KilledEnemiesCount,
                Currency = _currencyBank.Total,
                EarnedMetaCurrency = earnedMetaCurrency,
            };
            
            _windowsManager.Open(WindowType.EndGame);
            
            _gameplayAnalytics.GameOver();
        }

        private void StopWaves() => _wavesController.Stop();

        private void ApplySaveData(int earnedMetaCurrency, int wavesCount)
        {
            var data = _saveService.GetSaveData();

            data.MetaCurrency += earnedMetaCurrency;
            
            if (data.WavesRecord < wavesCount)
                data.WavesRecord = wavesCount;
            
            _saveService.Save(data);
        }

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