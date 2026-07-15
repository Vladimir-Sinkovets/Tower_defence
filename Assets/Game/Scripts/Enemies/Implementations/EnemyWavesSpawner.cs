using System.Threading;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Enemies.Implementations
{
    public class EnemyWavesSpawner : IEnemyWavesSpawner
    {
        private const float NavMeshSamplePositionMaxDistance = 2.0f;
        
        private readonly IEnemyFactory _enemyFactory;
        private readonly WavesConfig _wavesConfig;
        private readonly Transform[] _perimeterPoints;
        private bool _isStopped;
        private readonly GameSettings _gameSettings;

        public bool IsSpawning { get; private set; }
        
        public EnemyWavesSpawner(IEnemyFactory enemyFactory, WavesConfig wavesConfig, Transform[] perimeterPoints, GameSettingsService gameSettingsService)
        {
            _enemyFactory = enemyFactory;
            _wavesConfig = wavesConfig;
            _perimeterPoints = perimeterPoints;
            _gameSettings = gameSettingsService.GameSettings;
        }

        public async UniTask SpawnWave(int count, Health targetHealth, Transform targetTransform, CancellationToken ct)
        {
            IsSpawning = true;

            for (int i = 0; i < count; i++)
            {
                await UniTask.WaitWhile(() => _isStopped, cancellationToken: ct);

                var spawnPoint = GetRandomPerimeterPoint();

                var enemy = await _enemyFactory.Create(_wavesConfig.EnemyConfig);

                enemy.transform.position = spawnPoint;

                enemy.Init(_gameSettings.WavesSettings.EnemySettings, targetHealth, targetTransform);

                enemy.Activate();

                await UniTask.WaitWhile(() => _isStopped, cancellationToken: ct);
                
                await UniTask.WaitForSeconds(_gameSettings.WavesSettings.IntervalBetweenEnemies, cancellationToken: ct);
            }

            IsSpawning = false;
        }

        public void Stop() => _isStopped = true;
        public void Resume() => _isStopped = false;

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