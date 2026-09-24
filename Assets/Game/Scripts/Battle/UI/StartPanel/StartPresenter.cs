using System;
using Assets.Game.Scripts.Battle.Services.EnemySpawnStarters;

namespace Assets.Game.Scripts.Battle.UI.StartPanel
{
    public class StartPresenter : IDisposable
    {
        private readonly IStartView _view;
        private readonly IEnemySpawnStarter _enemySpawnStarter;

        public StartPresenter(IStartView view, IEnemySpawnStarter enemySpawnStarter)
        {
            _view = view;
            _enemySpawnStarter = enemySpawnStarter;
        }
        
        public void Init() => _view.OnStartButtonClicked += OnStartButtonClickedHandler;

        private void OnStartButtonClickedHandler()
        {
            _enemySpawnStarter.Start();

            _view.Hide();
        }
        
        public void Dispose() => _view.OnStartButtonClicked -= OnStartButtonClickedHandler;
    }
}