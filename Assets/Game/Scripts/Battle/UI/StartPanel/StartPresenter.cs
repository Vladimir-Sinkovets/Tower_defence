using System;
using Assets.Game.Scripts.Battle.Ecs.Spawn;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.UI.StartPanel
{
    public class StartPresenter : IDisposable
    {
        private readonly IStartView _view;
        private readonly World _world;

        public StartPresenter(IStartView view, World world)
        {
            _view = view;
            _world = world;
        }
        
        public void Init() => _view.OnStartButtonClicked += OnStartButtonClickedHandler;

        private void OnStartButtonClickedHandler()
        {
            _world.GetStash<EnemySpawner>().Set(
                _world.CreateEntity(),
                new()
                {
                    
                });

            _view.Hide();
        }
        
        public void Dispose() => _view.OnStartButtonClicked -= OnStartButtonClickedHandler;
    }
}