using System;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Arena.UI.Windows.EndGame
{
    public class EndGamePresenter : IDisposable, IWindowPresenter
    {
        private readonly IEndGameView _view;
        private readonly ISceneLoader _sceneLoader;
        // private readonly IGameResultCalculator _gameResultCalculator;

        public EndGamePresenter(
            IEndGameView view,
            ISceneLoader sceneLoader)
        {
            _view = view;
            _sceneLoader = sceneLoader;
        }

        public void Activate()
        {
            _view.OnMenuButtonClicked += OnMenuButtonClickedHandler;
            
            // var result = _gameResultCalculator.GameOverResult;
            
            _view.Open();
            _view.ShowKillsCount(11111/*result.Kills*/);
            _view.ShowEarnedMetaCurrency(22222/*result.EarnedMetaCurrency*/);
        }
        
        public void Deactivate()
        {
            _view.OnMenuButtonClicked -= OnMenuButtonClickedHandler;

            _view.Close();
        }

        private void OnMenuButtonClickedHandler()
        {
            _sceneLoader.LoadScene(SceneNames.Menu);
        }

        public void Dispose() => Deactivate();
    }
}