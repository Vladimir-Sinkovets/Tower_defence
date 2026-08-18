using System;
using System.Threading;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Assets.Game.Scripts.Enemies.Implementations
{
    public class EnemyWavesSpawner : IEnemyWavesSpawner
    {
        public event Action OnSpawnCompleted;
        private const float NavMeshSamplePositionMaxDistance = 2.0f;
        
        private readonly IEnemyFactory _enemyFactory;
        private readonly WavesConfig _wavesConfig;
        private readonly Transform[] _perimeterPoints;
        private readonly GameSettings _settings;

        private UniTaskCompletionSource _resumeTcs;

        public bool IsSpawning { get; private set; }
        
        public EnemyWavesSpawner(IEnemyFactory enemyFactory, WavesConfig wavesConfig, Transform[] perimeterPoints, IGameSettingsAccessor gameSettingsAccessor)
        {
            _enemyFactory = enemyFactory;
            _wavesConfig = wavesConfig;
            _perimeterPoints = perimeterPoints;
            _settings = gameSettingsAccessor.Settings;
        }

        public async UniTask SpawnWaveAsync(int count, Health targetHealth, Transform targetTransform, CancellationToken ct)
        {
            IsSpawning = true;
            
            for (int i = 0; i < count; i++)
            {
                await WaitIfPaused(ct);

                var spawnPoint = GetRandomPerimeterPoint();

                var enemy = await _enemyFactory.CreateAsync(_wavesConfig.EnemyConfig);

                enemy.transform.position = spawnPoint;

                enemy.Init(_settings.WavesSettings.EnemySettings, targetHealth, targetTransform);

                enemy.Activate();

                await WaitIfPaused(ct);
                
                await UniTask.WaitForSeconds(_settings.WavesSettings.IntervalBetweenEnemies, cancellationToken: ct);
            }

            IsSpawning = false;
            OnSpawnCompleted?.Invoke();
        }

        public void Stop() => _resumeTcs ??= new UniTaskCompletionSource();

        public void Resume()
        {
            if (_resumeTcs == null)
                return;
                
            _resumeTcs.TrySetResult();
            _resumeTcs = null;
        }

        private async UniTask WaitIfPaused(CancellationToken ct)
        {
            if (_resumeTcs == null)
                return;

            await _resumeTcs.Task.AttachExternalCancellation(ct);
        }

        private Vector3 GetRandomPerimeterPoint()
        {
            var index = Random.Range(0, _perimeterPoints.Length);

            var firstRandomPoint = _perimeterPoints[index];
            var secondRandomPoint = index + 1 < _perimeterPoints.Length ?
                _perimeterPoints[index + 1] :
                _perimeterPoints[0];

            var spawnPos = Vector3.Lerp(firstRandomPoint.position, secondRandomPoint.position, Random.value);

            if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, NavMeshSamplePositionMaxDistance, NavMesh.AllAreas))
                return hit.position;

            return firstRandomPoint.position;
        }
    }
}