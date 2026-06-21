using System;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.CurrencyBanks;
using Assets.Game.Scripts.Services.GameResultSavers;
using Assets.Game.Scripts.Services.GameStoppers;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Services.RewardCalculators;
using Assets.Game.Scripts.Services.Statistics;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI.Windows;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.GameOverManagers
{
    public class GameOverManager : IDisposable
    {
        private readonly GameStatistics _gameStatistics;
        private readonly CurrencyBank _currencyBank;
        private readonly IWindowsManager _windowsManager;
        private readonly IAnalytics _analytics;
        private readonly IGameResultSaver _gameResultSaver;
        private readonly IGameStopper _gameStopper;
        private readonly IRewardCalculator _rewardCalculator;
        private readonly IWavesController _wavesController;

        private Health _castleHealth;

        public GameOverResult GameOverResult { get; private set; }

        public GameOverManager(
            IGameResultSaver gameResultSaver, 
            IGameStopper gameStopper,
            IRewardCalculator rewardCalculator,
            IWavesController waveController,
            GameStatistics gameStatistics,
            CurrencyBank currencyBank,
            IWindowsManager windowsManager,
            IAnalytics analytics)
        {
            _gameResultSaver = gameResultSaver;
            _gameStopper = gameStopper;
            _rewardCalculator = rewardCalculator;
            _wavesController = waveController;
            _gameStatistics = gameStatistics;
            _currencyBank = currencyBank;
            _windowsManager = windowsManager;
            _analytics = analytics;
        }

        public void Init(Health castleHealth)
        {
            _castleHealth = castleHealth;
            castleHealth.OnDied += OnDiedHandler;
        }

        private void OnDiedHandler() => GameOver();

        private void GameOver()
        {
            _gameStopper.Stop();

            var earnedMetaCurrency = _rewardCalculator.CalculateMetaCurrency(_wavesController.WavesCount, _gameStatistics.KilledEnemiesCount);

            _gameResultSaver.ApplySaveData(earnedMetaCurrency, _wavesController.WavesCount);
            
            GameOverResult = new GameOverResult()
            {
                Waves = _wavesController.WavesCount,
                Kills = _gameStatistics.KilledEnemiesCount,
                Currency = _currencyBank.Total,
                EarnedMetaCurrency = earnedMetaCurrency,
            };
            
            _windowsManager.Open(WindowType.EndGame).Forget();
            
            _analytics.GameOver();
        }

        public void Dispose() => _castleHealth.OnDied -= OnDiedHandler;
    }

    public class GameOverResult
    {
        public int Waves;
        public int Kills;
        public int Currency;
        public int EarnedMetaCurrency;
    }
}