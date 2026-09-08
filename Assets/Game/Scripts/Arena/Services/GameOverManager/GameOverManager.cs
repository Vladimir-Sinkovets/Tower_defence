using System;
using Assets.Game.Scripts.Arena.Services.ArenaContexts;
using Assets.Game.Scripts.Arena.Services.GameResultCalculators;
using Assets.Game.Scripts.Arena.UI.Windows;
using Assets.Game.Scripts.Services.GameResultSavers;
using Assets.Game.Scripts.Services.Net;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Arena.Services.GameOverManager
{
    public class GameOverManager : IGameOverManager, IDisposable
    {
        private readonly IWindowsManager _windowsManager;
        private readonly IGameResultCalculator _gameResultCalculator;
        private readonly IGameResultSaver _gameResultSaver;
        private readonly IPlayerAccessor _playerAccessor;
        private readonly INetworkService _networkService;

        private Health _playerHealth;

        public GameOverManager(
            IWindowsManager windowsManager,
            IGameResultCalculator gameResultCalculator,
            IGameResultSaver gameResultSaver,
            IPlayerAccessor playerAccessor,
            INetworkService networkService)
        {
            _windowsManager = windowsManager;
            _gameResultCalculator = gameResultCalculator;
            _gameResultSaver = gameResultSaver;
            _playerAccessor = playerAccessor;
            _networkService = networkService;
        }

        public void Init(Health castleHealth)
        {
            _playerHealth = castleHealth;
            castleHealth.OnDied += CastleDiedHandler;
        }

        private void CastleDiedHandler()
        {
            var result = _gameResultCalculator.Calculate();
            
            _gameResultSaver.ApplyMetaCurrency(result.EarnedMetaCurrency);

            _windowsManager.CloseAll();
            
            _networkService.DisconnectRoom();
            
            _windowsManager.Open(WindowType.EndGame);
        }

        public void Dispose() => _playerHealth.OnDied -= CastleDiedHandler;
    }
}