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
        private readonly IEnemyWavesSpawner _enemyWavesController;
        private readonly Registry<Enemy> _enemyRegistry;
        private readonly IAnalytics _analytics;
        private readonly GameSettings _gameSettings;
        
        public int WavesCount { get; private set; }

        private CancellationTokenSource _wavesCts;
        private int _aliveEnemyCount;
        private bool _isStopped;

        public WavesController(
            IEnemyWavesSpawner enemyWavesSpawner,
            Registry<Enemy> enemyRegistry,
            IAnalytics analytics,
            GameSettingsService gameSettingsService)
        {
            _enemyWavesController = enemyWavesSpawner;
            _enemyRegistry = enemyRegistry;
            _analytics = analytics;
            _gameSettings = gameSettingsService.GameSettings;
        }

        public void Initialize()
        {
            _enemyRegistry.OnRegistered += OnRegisteredHandler;
            _enemyRegistry.OnUnregistered += OnUnregisteredHandler;
        }

        public void StartWaves(Health targetHealth, Transform targetTransform)
        {
            _wavesCts?.Cancel();
            _wavesCts?.Dispose();
            
            _wavesCts = new CancellationTokenSource();
            
            SpawnWaves(targetHealth, targetTransform, _wavesCts.Token).Forget();
        }

        public void Stop()
        {
            _isStopped = true;
            _enemyWavesController.Stop();
        }

        public void Resume()
        {
            _isStopped = false;
            _enemyWavesController.Resume();
        }

        private async UniTaskVoid SpawnWaves(Health targetHealth, Transform targetTransform, CancellationToken ct)
        {
            while (ct.IsCancellationRequested == false)
            {
                await UniTask.WaitWhile(() => _isStopped, cancellationToken: _wavesCts.Token);
                
                var enemyCount = _gameSettings.WavesSettings.BaseEnemyCount + WavesCount * _gameSettings.WavesSettings.NewEnemiesPerWave;
                
                _analytics.WaveStarted(WavesCount, enemyCount);

                await _enemyWavesController.SpawnWave(enemyCount, targetHealth, targetTransform, ct);

                await UniTask.WaitUntil(() => 
                    _enemyWavesController.IsSpawning == false &&
                    _aliveEnemyCount == 0,
                    cancellationToken: ct);

                _analytics.WaveCompleted(WavesCount, enemyCount);
                
                await UniTask.WaitWhile(() => _isStopped, cancellationToken: _wavesCts.Token);
                
                await UniTask.WaitForSeconds(_gameSettings.WavesSettings.IntervalBetweenWaves, cancellationToken: ct);

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
            _enemyRegistry.OnRegistered -= OnRegisteredHandler;
            _enemyRegistry.OnUnregistered -= OnUnregisteredHandler;
            
            _wavesCts?.Cancel();
            _wavesCts?.Dispose();
            _wavesCts = null;
        }
    }
}