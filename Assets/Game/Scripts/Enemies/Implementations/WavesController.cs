using System;
using System.Linq;
using System.Threading;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Enemies.Implementations
{
    public class WavesController : IWavesController, IDisposable
    {
        private readonly IEnemyWavesSpawner _enemyWavesController;
        private readonly Registry<Enemy> _enemyRegistry;
        private readonly IAnalytics _analytics;
        private readonly WavesConfig _wavesConfig;
        
        public int WavesCount { get; private set; }

        private CancellationTokenSource _wavesCts;
        private int _aliveEnemyCount;

        public WavesController(
            IEnemyWavesSpawner enemyWavesSpawner,
            WavesConfig wavesConfig,
            Registry<Enemy> enemyRegistry,
            IAnalytics analytics)
        {
            _enemyWavesController = enemyWavesSpawner;
            _enemyRegistry = enemyRegistry;
            _analytics = analytics;
            _wavesConfig = wavesConfig;
        }

        public void StartWaves(Health target)
        {
            _wavesCts?.Cancel();
            _wavesCts?.Dispose();
            
            _wavesCts = new CancellationTokenSource();

            _enemyRegistry.OnRegistered += OnRegisteredHandler;
            _enemyRegistry.OnUnregistered += OnUnregisteredHandler;
            
            SpawnWaves(target, _wavesCts.Token).Forget();
        }

        public void Stop() => _wavesCts?.Cancel();

        private async UniTaskVoid SpawnWaves(Health target, CancellationToken ct)
        {
            while (ct.IsCancellationRequested == false)
            {
                var enemyCount = _wavesConfig.BaseEnemyCount + WavesCount * _wavesConfig.NewEnemiesPerWave;
                
                _analytics.WaveStarted(WavesCount, enemyCount);

                await _enemyWavesController.SpawnWave(enemyCount, target, ct);

                await UniTask.WaitUntil(() => 
                    _enemyWavesController.IsSpawning == false &&
                    _aliveEnemyCount == 0,
                    cancellationToken: ct);

                _analytics.WaveCompleted(WavesCount, enemyCount);
                
                await UniTask.WaitForSeconds(_wavesConfig.IntervalBetweenWaves, cancellationToken: ct);

                WavesCount++;
            }
        }

        
        private void OnRegisteredHandler(Enemy enemy)
        {
            _aliveEnemyCount++;
            enemy.OnDied += OnEnemyDiedHandler;
        }
        
        private void OnUnregisteredHandler(Enemy enemy) => enemy.OnDied -= OnEnemyDiedHandler;
        
        private void OnEnemyDiedHandler() => _aliveEnemyCount--;

        public void Dispose()
        {
            _wavesCts?.Cancel();
            _wavesCts?.Dispose();
            _wavesCts = null;
        }
    }
}