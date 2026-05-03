using System;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.UI.Windows.EndGame
{
    public class EndGamePresenter : IDisposable, IWindowPresenter
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
        }

        public void Activate()
        {
            var result = _gameOverManager.GameOverResult;
            
            _view.Open();
            _view.ShowWavesCount(result.Waves);
            _view.ShowKillsCount(result.Kills);
            _view.ShowCurrency(result.Currency);
            _view.ShowEarnedMetaCurrency(result.EarnedMetaCurrency);
        }

        public void Deactivate() { }

        private void OnRestartButtonClickedHandler() => _sceneLoader.LoadScene(SceneNames.Game);
        private void OnMenuButtonClickedHandler() => _sceneLoader.LoadScene(SceneNames.Menu);
        
        public void Dispose()
        {
            _view.OnMenuButtonClicked -= OnMenuButtonClickedHandler;
            _view.OnRestartButtonClicked -= OnRestartButtonClickedHandler;
        }
    }
}