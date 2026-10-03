using Assets.Game.Scripts.Battle.Services.BattleResultCalculators;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Battle.UI.Windows.EndGamePanel
{
    public class EndGamePresenter : IWindowPresenter
    {
        private readonly IEndGameView _view;
        private readonly ISceneLoader _sceneLoader;
        private readonly IBattleResultCalculator _gameResultCalculator;

        public EndGamePresenter(
            IEndGameView view,
            ISceneLoader sceneLoader,
            IBattleResultCalculator gameResultCalculator)
        {
            _view = view;
            _sceneLoader = sceneLoader;
            _gameResultCalculator = gameResultCalculator;
        }

        public void Activate()
        {
            _view.OnMenuButtonClicked += OnMenuButtonClickedHandler;
            _view.OnRestartButtonClicked += OnRestartButtonClickedHandler;
            
            var result = _gameResultCalculator.GetGameOverResult();
            
            _view.Open();
            _view.ShowWavesCount(result.Waves);
            _view.ShowKillsCount(result.Kills);
            _view.ShowEarnedMetaCurrency(result.EarnedMetaCurrency);
        }
        
        public void Deactivate()
        {
            _view.OnMenuButtonClicked -= OnMenuButtonClickedHandler;
            _view.OnRestartButtonClicked -= OnRestartButtonClickedHandler;

            _view.Close();
        }

        private void OnRestartButtonClickedHandler() => _sceneLoader.LoadScene(SceneNames.Battle);

        private void OnMenuButtonClickedHandler() => _sceneLoader.LoadScene(SceneNames.Menu);

        public void Dispose() => Deactivate();
    }
}