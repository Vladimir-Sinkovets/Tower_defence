using System;
using Assets.Game.Scripts.Configs;
using System.Linq;
using System.Threading;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Enemies.Implementations
{
    public class WavesController : IWavesController, IDisposable
    {
        private readonly IEnemyWavesSpawner _enemyWavesController;
        private readonly Registry<Enemy> _enemyRegistry;
        private readonly IGameplayAnalytics _gameplayAnalytics;
        private readonly WavesConfig _wavesConfig;
        
        public int WavesCount { get; private set; }

        private CancellationTokenSource _wavesCts;

        public WavesController(
            IEnemyWavesSpawner enemyWavesSpawner,
            WavesConfig wavesConfig,
            Registry<Enemy> enemyRegistry,
            IGameplayAnalytics gameplayAnalytics)
        {
            _enemyWavesController = enemyWavesSpawner;
            _enemyRegistry = enemyRegistry;
            _gameplayAnalytics = gameplayAnalytics;
            _wavesConfig = wavesConfig;
        }

        public void StartWaves(Health target)
        {
            _wavesCts?.Cancel();
            _wavesCts?.Dispose();
            
            _wavesCts = new CancellationTokenSource();
            
            SpawnWaves(target, _wavesCts.Token).Forget();
        }

        public void Stop() => _wavesCts?.Cancel();

        private async UniTaskVoid SpawnWaves(Health target, CancellationToken ct)
        {
            while (ct.IsCancellationRequested == false)
            {
                var enemyCount = _wavesConfig.BaseEnemyCount + WavesCount * _wavesConfig.NewEnemiesPerWave;
                
                _gameplayAnalytics.WaveStarted(WavesCount, enemyCount);

                await _enemyWavesController.SpawnWave(enemyCount, target, ct);

                await UniTask.WaitUntil(() => 
                    _enemyWavesController.IsSpawning == false &&
                    _enemyRegistry.All.Any(x => !x.IsDead) == false,
                    cancellationToken: ct);

                _gameplayAnalytics.WaveCompleted(WavesCount, enemyCount);
                
                await UniTask.WaitForSeconds(_wavesConfig.IntervalBetweenWaves, cancellationToken: ct);

                WavesCount++;
            }
        }

        public void Dispose()
        {
            _wavesCts?.Cancel();
            _wavesCts?.Dispose();
            _wavesCts = null;
        }
    }
}