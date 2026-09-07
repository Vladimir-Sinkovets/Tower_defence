using System;
using Assets.Game.Scripts.Arena.UI.Windows;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Arena.Services.GameOverManager
{
    public class GameOverManager : IGameOverManager, IDisposable
    {
        private readonly IWindowsManager _windowsManager;
        // private readonly IGameResultCalculator _gameResultCalculator;
        // private readonly IGameResultSaver _gameResultSaver;

        private Health _playerHealth;

        public GameOverManager(IWindowsManager windowsManager)
        {
            _windowsManager = windowsManager;
        }

        public void Init(Health castleHealth)
        {
            _playerHealth = castleHealth;
            castleHealth.OnDied += CastleDiedHandler;
        }

        private void CastleDiedHandler()
        {
            // var result = _gameResultCalculator.Calculate();
            
            // _gameResultSaver.ApplyMetaCurrency(result.EarnedMetaCurrency);
            // _gameResultSaver.ApplyWavesRecord(result.Waves);

            _windowsManager.CloseAll();
            
            _windowsManager.Open(WindowType.EndGame);
        }

        public void Dispose() => _playerHealth.OnDied -= CastleDiedHandler;
    }
}