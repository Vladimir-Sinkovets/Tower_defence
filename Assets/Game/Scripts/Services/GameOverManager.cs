using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI;
using System;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class GameOverManager : IDisposable
    {
        private Registry<Enemy> _enemyRegistry;
        private Registry<Building> _buildingRegistry;
        private EndGamePanel _endGamePanel;
        private GameStatistics _gameStatistics;
        private CurrencyBank _currencyBank;
        private MetaCurrencyConfig _metaCurrencyConfig;
        private MetaCurrencyService _metaCurrencyService;
        private SceneLoader _sceneLoader;
        private WavesController _wavesController;
        private BuildingController _buildingController;

        [Inject]
        public void Construct(
            WavesController waveController,
            BuildingController buildingController,
            Registry<Building> buildingRegistry,
            Registry<Enemy> enemyRegistry,
            EndGamePanel endGamePanel,
            GameStatistics gameStatistics,
            CurrencyBank currencyBank,
            MetaCurrencyConfig metaCurrencyConfig,
            MetaCurrencyService metaCurrencyService,
            SceneLoader sceneLoader)
        {
            _wavesController = waveController;
            _buildingController = buildingController;
            _enemyRegistry = enemyRegistry;
            _buildingRegistry = buildingRegistry;
            _endGamePanel = endGamePanel;
            _gameStatistics = gameStatistics;
            _currencyBank = currencyBank;
            _metaCurrencyConfig = metaCurrencyConfig;
            _metaCurrencyService = metaCurrencyService;
            _sceneLoader = sceneLoader;

            _endGamePanel.OnRestartButtonClicked += OnRestartClickHandler;
            _endGamePanel.OnMenuButtonClicked += OnMenuClickHandler;
        }

        public void GameOver()
        {
            StopEnemies();

            StopBuildings();

            StopBuildingController();

            var earnedMetaCurrency = CalculateMetaData();

            ApplyMetaData(earnedMetaCurrency);

            OpenPanel(earnedMetaCurrency);
        }

        private void OnMenuClickHandler() => _sceneLoader.LoadScene(SceneNames.Menu);
        private void OnRestartClickHandler() => _sceneLoader.ReloadCurrentScene();

        private void ApplyMetaData(int value) => _metaCurrencyService.Add(value);

        private int CalculateMetaData() => _wavesController.WavesCount * _metaCurrencyConfig.MetaCurrencyPerWave +
                _gameStatistics.KilledEnemiesCount * _metaCurrencyConfig.MetaCurrencyPerKill;

        private void OpenPanel(int earnedMetaCurrency) => _endGamePanel.Open(
            _wavesController.WavesCount,
            _gameStatistics.KilledEnemiesCount,
            _currencyBank.Total,
            earnedMetaCurrency);

        private void StopBuildingController() => _buildingController.Stop();

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
            _endGamePanel.OnRestartButtonClicked -= OnRestartClickHandler;
            _endGamePanel.OnMenuButtonClicked -= OnMenuClickHandler;
        }
    }
}