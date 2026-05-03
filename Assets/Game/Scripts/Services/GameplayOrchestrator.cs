using System;
using System.Threading;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services
{
    public class GameplayOrchestrator : IDisposable
    {
        private readonly WavesController _wavesController;
        private readonly GameOverManager _gameOverManager;
        private readonly FieldStartupAnimation _fieldStartupAnimation;
        private readonly HudFactory _hudFactory;
        private readonly CastleFactory _castleFactory;
        
        private CancellationTokenSource _startGameCts;
        
        private Health _castleHealth;

        public GameplayOrchestrator(
            WavesController waveController,
            GameOverManager gameOverManager,
            FieldStartupAnimation fieldStartupAnimation,
            HudFactory hudFactory,
            CastleFactory castleFactory)
        {
            _wavesController = waveController;
            _gameOverManager = gameOverManager;
            _fieldStartupAnimation = fieldStartupAnimation;
            _hudFactory = hudFactory;
            _castleFactory = castleFactory;
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