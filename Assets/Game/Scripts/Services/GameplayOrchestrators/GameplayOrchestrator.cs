using System;
using System.Threading;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.CastleFactories;
using Assets.Game.Scripts.Services.GameOverManagers;
using Assets.Game.Scripts.Services.GameResumeServices;
using Assets.Game.Scripts.Services.HudFactories;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.GameplayOrchestrators
{
    public class GameplayOrchestrator : IDisposable
    {
        private readonly IWavesController _wavesController;
        private readonly GameOverManager _gameOverManager;
        private readonly IGameResumeService _gameResumeService;
        private readonly FieldStartupAnimation _fieldStartupAnimation;
        private readonly HudFactory _hudFactory;
        private readonly CastleFactory _castleFactory;
        private readonly IAnalytics _analytics;

        private CancellationTokenSource _startGameCts;

        public GameplayOrchestrator(
            IWavesController waveController,
            GameOverManager gameOverManager,
            IGameResumeService gameResumeService,
            FieldStartupAnimation fieldStartupAnimation,
            HudFactory hudFactory,
            CastleFactory castleFactory,
            IAnalytics analytics)
        {
            _wavesController = waveController;
            _gameOverManager = gameOverManager;
            _gameResumeService = gameResumeService;
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
            
            var (castleHealth, castleTransform) = await _castleFactory.CreateCastle(ct);

            await _hudFactory.CreateHUD(castleHealth);

            _wavesController.StartWaves(castleHealth, castleTransform);
            
            _gameOverManager.Init(castleHealth);

            _gameResumeService.Init(castleHealth);
        }

        public void Dispose()
        {
            _startGameCts?.Cancel();
            _startGameCts?.Dispose();
            _startGameCts = null;
        }
    }
}