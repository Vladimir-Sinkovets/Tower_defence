using System;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.GameResults;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.UI.Windows.EndGame
{
    public class EndGamePresenter : IDisposable, IWindowPresenter
    {
        private readonly IEndGameView _view;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameResultCalculator _gameResultCalculator;
        private readonly IAnalytics _analytics;

        public EndGamePresenter(
            IEndGameView view,
            ISceneLoader sceneLoader,
            IGameResultCalculator gameResultCalculator,
            IAnalytics analytics)
        {
            _view = view;
            _sceneLoader = sceneLoader;
            _gameResultCalculator = gameResultCalculator;
            _analytics = analytics;
        }

        public void Activate()
        {
            _view.OnMenuButtonClicked += OnMenuButtonClickedHandler;
            _view.OnRestartButtonClicked += OnRestartButtonClickedHandler;
            
            var result = _gameResultCalculator.GameOverResult;
            
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

            _view.Close();
        }

        private void OnRestartButtonClickedHandler()
        {
            _analytics.SessionRestarted();
            
            _sceneLoader.LoadScene(SceneNames.Game);
        }

        private void OnMenuButtonClickedHandler()
        {
            _analytics.ReturnedToMenu();
            
            _sceneLoader.LoadScene(SceneNames.Menu);
        }

        public void Dispose() => Deactivate();
    }
}