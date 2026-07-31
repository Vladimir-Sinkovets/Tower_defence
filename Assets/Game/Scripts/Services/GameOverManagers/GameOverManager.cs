using System;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.GameResults;
using Assets.Game.Scripts.Services.GameResultSavers;
using Assets.Game.Scripts.Services.GameStoppers;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI.Windows;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.GameOverManagers
{
    public class GameOverManager : IGameOverManager, IDisposable
    {
        private readonly IWindowsManager _windowsManager;
        private readonly IGameResultCalculator _gameResultCalculator;
        private readonly IGameResultSaver _gameResultSaver;
        private readonly IAnalytics _analytics;
        private readonly IGameStopper _gameStopper;

        private Health _castleHealth;

        public GameOverManager(
            IGameStopper gameStopper,
            IWindowsManager windowsManager,
            IGameResultCalculator gameResultCalculator,
            IGameResultSaver gameResultSaver,
            IAnalytics analytics)
        {
            _gameStopper = gameStopper;
            _windowsManager = windowsManager;
            _gameResultCalculator = gameResultCalculator;
            _gameResultSaver = gameResultSaver;
            _analytics = analytics;
        }

        public void Init(Health castleHealth)
        {
            _castleHealth = castleHealth;
            castleHealth.OnDied += CastleDiedHandler;
        }

        public void GameOver()
        {
            var result = _gameResultCalculator.Calculate();
            
            _gameResultSaver.ApplyMetaCurrency(result.EarnedMetaCurrency);
            _gameResultSaver.ApplyWavesRecord(result.Waves);

            _windowsManager.Open(WindowType.EndGame).Forget();
            
            _analytics.GameOver();
        }
        
        private void CastleDiedHandler()
        {
            _gameStopper.Stop();
            
            _windowsManager.CloseAll();
            
            _windowsManager.Open(WindowType.ContinueByAd).Forget();
        }

        public void Dispose() => _castleHealth.OnDied -= CastleDiedHandler;
    }
}