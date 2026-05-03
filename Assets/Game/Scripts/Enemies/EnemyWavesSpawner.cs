using Assets.Game.Scripts.Shared;
using System.Threading;
using Assets.Game.Scripts.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts.Enemies
{
    public class EnemyWavesSpawner
    {
        private readonly IInstantiator _instantiator;
        private readonly WavesConfig _wavesConfig;
        private readonly Transform[] _perimeterPoints;
        
        public bool IsSpawning { get; private set; }
        
        public EnemyWavesSpawner(IInstantiator instantiator, WavesConfig wavesConfig, Transform[] perimeterPoints)
        {
            _instantiator = instantiator;
            _wavesConfig = wavesConfig;
            _perimeterPoints = perimeterPoints;
        }

        public async UniTask SpawnWave(int count, Health target, CancellationToken ct)
        {
            IsSpawning = true;

            for (int i = 0; i < count; i++)
            {
                var spawnPoint = GetRandomPerimeterPoint();

                var enemy = _wavesConfig.EnemyFactory.Create(_instantiator);

                enemy.transform.position = spawnPoint;

                enemy.Activate(target);

                await UniTask.WaitForSeconds(_wavesConfig.IntervalBetweenEnemies, cancellationToken: ct);
            }

            IsSpawning = false;
        }

        private Vector3 GetRandomPerimeterPoint()
        {
            var index = Random.Range(0, _perimeterPoints.Length);

            var firstRandomPoint = _perimeterPoints[index];
            var secondRandomPoint = index + 1 < _perimeterPoints.Length ?
                _perimeterPoints[index + 1] :
                _perimeterPoints[0];

            var spawnPos = Vector3.Lerp(firstRandomPoint.position, secondRandomPoint.position, Random.value);

            if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                return hit.position;

            return firstRandomPoint.position;
        }
    }
}