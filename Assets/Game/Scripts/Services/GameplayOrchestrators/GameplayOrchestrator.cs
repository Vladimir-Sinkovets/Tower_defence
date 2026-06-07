using System;
using System.Threading;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.CastleFactories;
using Assets.Game.Scripts.Services.GameOverManagers;
using Assets.Game.Scripts.Services.HudFactories;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.GameplayOrchestrators
{
    public class GameplayOrchestrator : IDisposable
    {
        private readonly IWavesController _wavesController;
        private readonly GameOverManager _gameOverManager;
        private readonly FieldStartupAnimation _fieldStartupAnimation;
        private readonly HudFactory _hudFactory;
        private readonly CastleFactory _castleFactory;
        private readonly IAnalytics _analytics;

        private CancellationTokenSource _startGameCts;
        
        private Health _castleHealth;

        public GameplayOrchestrator(
            IWavesController waveController,
            GameOverManager gameOverManager,
            FieldStartupAnimation fieldStartupAnimation,
            HudFactory hudFactory,
            CastleFactory castleFactory,
            IAnalytics analytics)
        {
            _wavesController = waveController;
            _gameOverManager = gameOverManager;
            _fieldStartupAnimation = fieldStartupAnimation;
            _hudFactory = hudFactory;
            _castleFactory = castleFactory;
            _analytics = analytics;
        }

        public void Init()
        {
            _startGameCts?.Cancel();
            _startGameCts?.Dispose();
            _startGameCts = new CancellationTokenSource();
            
            StartGame(_startGameCts.Token).Forget();
        }

        private async UniTaskVoid StartGame(CancellationToken ct)
        {
            _analytics.GameStarted();
            
            await _fieldStartupAnimation.Play(ct);
            
            _castleHealth = await _castleFactory.CreateCastle(ct);

            _hudFactory.CreateHUD(_castleHealth);

            _wavesController.StartWaves(_castleHealth);
            
            _gameOverManager.Init(_castleHealth);
        }

        public void Dispose()
        {
            _startGameCts?.Cancel();
            _startGameCts?.Dispose();
            _startGameCts = null;
        }
    }
}