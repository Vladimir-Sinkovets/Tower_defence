using System;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.UI
{
    public class EndGamePresenter : IDisposable
    {
        private readonly IEndGameView _view;
        private readonly SceneLoader _sceneLoader;
        private readonly GameOverManager _gameOverManager;

        public EndGamePresenter(
            IEndGameView view,
            SceneLoader sceneLoader,
            GameOverManager gameOverManager)
        {
            _view = view;
            _sceneLoader = sceneLoader;
            _gameOverManager = gameOverManager;
            
            _view.OnMenuButtonClicked += OnMenuButtonClickedHandler;
            _view.OnRestartButtonClicked += OnRestartButtonClickedHandler;

            _gameOverManager.OnGameOver += OnGameOverHandler;
        }

        private void OnGameOverHandler(GameOverResult result)
        {
            _view.Open();
            _view.ShowWavesCount(result.Waves);
            _view.ShowKillsCount(result.Kills);
            _view.ShowCurrency(result.Currency);
            _view.ShowEarnedMetaCurrency(result.EarnedMetaCurrency);
        }

        private void OnRestartButtonClickedHandler() => _sceneLoader.LoadScene(SceneNames.Game);
        private void OnMenuButtonClickedHandler() => _sceneLoader.LoadScene(SceneNames.Menu);
        
        public void Dispose()
        {
            _view.OnMenuButtonClicked -= OnMenuButtonClickedHandler;
            _view.OnRestartButtonClicked -= OnRestartButtonClickedHandler;

            _gameOverManager.OnGameOver -= OnGameOverHandler;
        }
    }
}