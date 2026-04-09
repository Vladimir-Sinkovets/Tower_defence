using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Services;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts
{
    public class EnemyWavesSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private Health _target;
        [SerializeField] private Transform[] _perimeterPoints;

        private GameContext _gameContext;
        private DiContainer _diContainer;
        private readonly WaitForSeconds _interval = new WaitForSeconds(1.0f);

        private bool _isSpawning;

        public bool IsSpawning { get => _isSpawning; }

        [Inject]
        private void Construct(GameContext context, DiContainer diContainer)
        {
            _gameContext = context;
            _diContainer = diContainer;
        }

        public IEnumerator SpawnWave(int count)
        {
            _isSpawning = true;

            for (int i = 0; i < count; i++)
            {
                var spawnPoint = GetRandomPerimeterPoint();

                var enemy = _enemyFactory.Create(_target, _diContainer);

                _gameContext.RegisterEnemy(enemy);

                enemy.transform.position = spawnPoint;

                yield return _interval;
            }

            _isSpawning = false;
        }

        private Vector3 GetRandomPerimeterPoint()
        {
            var index = Random.Range(0, _perimeterPoints.Length);

            var randomPoint_0 = _perimeterPoints[index];
            var randomPoint_1 = index + 1 < _perimeterPoints.Length ?
                _perimeterPoints[index + 1] :
                _perimeterPoints[0];

            var spawnPos = Vector3.Lerp(randomPoint_0.position, randomPoint_1.position, Random.Range(0.0f, 1.0f));

            if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return randomPoint_0.position;
        }


        private void OnDrawGizmos()
        {
            if (_perimeterPoints == null || _perimeterPoints.Length < 2)
                return;

            Gizmos.color = Color.yellow;

            for (int i = 0; i < _perimeterPoints.Length; i++)
            {
                if (i == _perimeterPoints.Length - 1)
                    Gizmos.DrawLine(_perimeterPoints[i].position, _perimeterPoints[0].position);
                else
                    Gizmos.DrawLine(_perimeterPoints[i].position, _perimeterPoints[i + 1].position);
            }
        }
    }
}