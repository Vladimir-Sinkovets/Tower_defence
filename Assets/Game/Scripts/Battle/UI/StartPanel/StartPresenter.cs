using System;
using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Services.GameStarters;
using Assets.Game.Scripts.Battle.Services.WorldAccessors;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.UI.StartPanel
{
    public class StartPresenter : IDisposable
    {
        private readonly IStartView _view;
        private readonly IGameStarter _gameStarter;
        private readonly Filter _units;

        public StartPresenter(IStartView view, IGameStarter gameStarter, IWorldAccessor worldAccessor)
        {
            _view = view;
            _gameStarter = gameStarter;

            _units = worldAccessor.World.Filter.With<Team>().Build();
        }
        
        public void Init() => _view.OnStartButtonClicked += OnStartButtonClickedHandler;

        private void OnStartButtonClickedHandler()
        {
            var count = 0;
            
            foreach (var _ in _units)
            {
                count++;
            }
            
            if (count == 0)
                return;
            
            _gameStarter.Start();

            _view.Hide();
        }
        
        public void Dispose() => _view.OnStartButtonClicked -= OnStartButtonClickedHandler;
    }
}