using System;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.GameOverManagers;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.UI.Windows.EndGame
{
    public class EndGamePresenter : IDisposable, IWindowPresenter
    {
        private readonly IEndGameView _view;
        private readonly SceneLoader _sceneLoader;
        private readonly GameOverManager _gameOverManager;
        private readonly IGameAnalytics _gameAnalytics;

        public EndGamePresenter(
            IEndGameView view,
            SceneLoader sceneLoader,
            GameOverManager gameOverManager,
            IGameAnalytics gameAnalytics)
        {
            _view = view;
            _sceneLoader = sceneLoader;
            _gameOverManager = gameOverManager;
            _gameAnalytics = gameAnalytics;
        }

        public void Activate()
        {
            _view.OnMenuButtonClicked += OnMenuButtonClickedHandler;
            _view.OnRestartButtonClicked += OnRestartButtonClickedHandler;
            
            var result = _gameOverManager.GameOverResult;
            
            _view.Open();
            _view.ShowWavesCount(result.Waves);
            _view.ShowKillsCount(result.Kills);
            _view.ShowCurrency(result.Currency);
            _view.ShowEarnedMetaCurrency(result.EarnedMetaCurrency);
        }

        public void Deactivate()
        {
            _view.OnMenuButtonClicked -= OnMenuButtonClickedHandler;
            _view.OnRestartButtonClicked -= OnRestartButtonClickedHandler;
        }

        private void OnRestartButtonClickedHandler()
        {
            _gameAnalytics.SessionRestarted();
            
            _sceneLoader.LoadScene(SceneNames.Game);
        }

        private void OnMenuButtonClickedHandler()
        {
            _gameAnalytics.ReturnedToMenu();
            
            _sceneLoader.LoadScene(SceneNames.Menu);
        }

        public void Dispose() => Deactivate();
    }
}