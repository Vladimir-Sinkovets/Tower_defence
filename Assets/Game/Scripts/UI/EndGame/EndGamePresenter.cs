using System;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.UI
{
    public class EndGamePresenter : IDisposable
    {
        private readonly IEndGameView _view;
        private readonly SceneLoader _sceneLoader;
        private readonly GameOverManager _gameOverManager;
        private readonly Castle _castle;
        private readonly WavesController _wavesController;
        private readonly GameStatistics _gameStatistics;
        private readonly CurrencyBank _currencyBank;
        private readonly MetaCurrencyService _metaCurrencyService;

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

        private void OnGameOverHandler(GameOverResult result) =>
            _view.Open(
                result.Waves, 
                result.Kills,
                result.Currency,
                result.EarnedMetaCurrency);

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