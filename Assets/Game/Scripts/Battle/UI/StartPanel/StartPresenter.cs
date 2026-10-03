using System;
using Assets.Game.Scripts.Battle.Services.GameStarters;

namespace Assets.Game.Scripts.Battle.UI.StartPanel
{
    public class StartPresenter : IDisposable
    {
        private readonly IStartView _view;
        private readonly IGameStarter _gameStarter;

        public StartPresenter(IStartView view, IGameStarter gameStarter)
        {
            _view = view;
            _gameStarter = gameStarter;
        }
        
        public void Init() => _view.OnStartButtonClicked += OnStartButtonClickedHandler;

        private void OnStartButtonClickedHandler()
        {
            _gameStarter.Start();

            _view.Hide();
        }
        
        public void Dispose() => _view.OnStartButtonClicked -= OnStartButtonClickedHandler;
    }
}