using System;
using Assets.Game.Scripts.Configs;
using System.Linq;
using System.Threading;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Enemies
{
    public class WavesController : IDisposable
    {
        private readonly EnemyWavesSpawner _enemyWavesController;
        private readonly Registry<Enemy> _enemyRegistry;
        private readonly WavesConfig _wavesConfig;
        
        public int WavesCount { get; private set; }

        private CancellationTokenSource _wavesCts;

        public WavesController(
            EnemyWavesSpawner enemyWavesSpawner,
            WavesConfig wavesConfig,
            Registry<Enemy> enemyRegistry)
        {
            _enemyWavesController = enemyWavesSpawner;
            _enemyRegistry = enemyRegistry;
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

                await _enemyWavesController.SpawnWave(enemyCount, target, ct);

                await UniTask.WaitUntil(() => 
                    _enemyWavesController.IsSpawning == false &&
                    _enemyRegistry.All.Any(x => !x.Health.IsDead) == false,
                    cancellationToken: ct);

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