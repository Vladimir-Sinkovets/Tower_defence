using Assets.Game.Scripts.Enemy;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Scripts
{
    public class EnemyWavesController : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private Health _target;
        [SerializeField] private Transform[] _perimeterPoints;

        private readonly WaitForSeconds _interval = new WaitForSeconds(1.0f);

        public void Init()
        {
            StartCoroutine(SpawnWaves());
        }

        public IEnumerator SpawnWaves()
        {
            for (int i = 0; i < 5; i++)
            {
                var spawnPoint = GetRandomPerimeterPoint();

                var enemy = _enemyFactory.Create(_target);

                enemy.transform.position = spawnPoint;

                yield return _interval;
            }

            yield break;
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

            return spawnPos;
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