using System;
using System.Threading;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Enemies.Implementations
{
    public class WavesController : IWavesController, IInitializable, IDisposable
    {
        private readonly IEnemyWavesSpawner _enemyWavesSpawner;
        private readonly Registry<Enemy> _enemyRegistry;
        private readonly IAnalytics _analytics;

        public int WavesNumber { get; private set; }

        private int _aliveEnemyCount;
        private bool _isStopped;
        private readonly GameSettings _settings;
        
        private CancellationTokenSource _wavesCts;
        private UniTaskCompletionSource _waveCompletedTcs;
        private UniTaskCompletionSource _resumeTcs;

        public WavesController(
            IEnemyWavesSpawner enemyWavesSpawner,
            Registry<Enemy> enemyRegistry,
            IAnalytics analytics,
            IGameSettingsAccessor gameSettingsAccessor)
        {
            _enemyWavesSpawner = enemyWavesSpawner;
            _enemyRegistry = enemyRegistry;
            _analytics = analytics;
            _settings = gameSettingsAccessor.Settings;
        }

        public void Initialize()
        {
            _enemyRegistry.OnRegistered += OnRegisteredHandler;
            _enemyRegistry.OnUnregistered += OnUnregisteredHandler;
            _enemyWavesSpawner.OnSpawnCompleted += OnSpawnCompletedHandler;
        }

        public void StartWaves(Health targetHealth, Transform targetTransform)
        {
            _wavesCts?.Cancel();
            _wavesCts?.Dispose();
            
            _wavesCts = new CancellationTokenSource();
            
            SpawnWavesAsync(targetHealth, targetTransform, _wavesCts.Token).Forget();
        }

        public void Stop()
        {
            _isStopped = true;
            _enemyWavesSpawner.Stop();
        }

        public void Resume()
        {
            _isStopped = false;
            _enemyWavesSpawner.Resume();
            _resumeTcs?.TrySetResult();
            _resumeTcs = null;
        }
        
        private async UniTaskVoid SpawnWavesAsync(Health targetHealth, Transform targetTransform, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _waveCompletedTcs = new UniTaskCompletionSource();
                
                await WaitIfStopped(ct);
                
                var enemyCount = _settings.WavesSettings.BaseEnemyCount + WavesNumber * _settings.WavesSettings.NewEnemiesPerWave;
                
                _analytics.WaveStarted(WavesNumber, enemyCount);

                await _enemyWavesSpawner.SpawnWaveAsync(enemyCount, targetHealth, targetTransform, ct);

                await _waveCompletedTcs.Task.AttachExternalCancellation(ct);

                _analytics.WaveCompleted(WavesNumber);
                
                await WaitBetweenWaves(_settings.WavesSettings.IntervalBetweenWaves, ct);

                WavesNumber++;
            }
        }
        
        private async UniTask WaitBetweenWaves(float duration, CancellationToken ct)
        {
            var elapsed = 0f;

            while (elapsed < duration)
            {
                await WaitIfStopped(ct);

                await UniTask.Yield(cancellationToken: ct);

                elapsed += Time.deltaTime;
            }
        }

        private async UniTask WaitIfStopped(CancellationToken ct)
        {
            if (!_isStopped)
                return;

            _resumeTcs ??= new UniTaskCompletionSource();

            await _resumeTcs.Task.AttachExternalCancellation(ct);
        }

        private void OnSpawnCompletedHandler() => TryCompleteWave();

        private void OnRegisteredHandler(Enemy enemy)
        {
            _aliveEnemyCount++;
            enemy.OnDied += OnEnemyDiedHandler;
        }
        
        private void OnUnregisteredHandler(Enemy enemy) => enemy.OnDied -= OnEnemyDiedHandler;
        
        private void OnEnemyDiedHandler(Enemy _)
        {
            _aliveEnemyCount--;
            TryCompleteWave();
        }
        
        private void TryCompleteWave()
        {
            if (_enemyWavesSpawner.IsSpawning || _aliveEnemyCount != 0)
                return;
            
            _waveCompletedTcs?.TrySetResult();
            _waveCompletedTcs = null;
        }

        public void Dispose()
        {
            _enemyRegistry.OnRegistered -= OnRegisteredHandler;
            _enemyRegistry.OnUnregistered -= OnUnregisteredHandler;
            _enemyWavesSpawner.OnSpawnCompleted -= OnSpawnCompletedHandler;
            
            _wavesCts?.Cancel();
            _wavesCts?.Dispose();
            _wavesCts = null;
        }
    }
}