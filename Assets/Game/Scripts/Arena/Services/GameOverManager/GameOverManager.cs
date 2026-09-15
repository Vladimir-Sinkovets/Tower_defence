using System;
using Assets.Game.Scripts.Arena.Player;
using Assets.Game.Scripts.Arena.Services.GameResultCalculators;
using Assets.Game.Scripts.Arena.UI.Windows;
using Assets.Game.Scripts.Services.GameResultSavers;
using Assets.Game.Scripts.Services.Net;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Arena.Services.GameOverManager
{
    public class GameOverManager : IGameOverManager, IDisposable
    {
        private readonly IWindowsManager _windowsManager;
        private readonly IGameResultCalculator _gameResultCalculator;
        private readonly IGameResultSaver _gameResultSaver;
        private readonly INetworkService _networkService;

        private Health _playerHealth;

        public GameOverManager(
            IWindowsManager windowsManager,
            IGameResultCalculator gameResultCalculator,
            IGameResultSaver gameResultSaver,
            INetworkService networkService)
        {
            _windowsManager = windowsManager;
            _gameResultCalculator = gameResultCalculator;
            _gameResultSaver = gameResultSaver;
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