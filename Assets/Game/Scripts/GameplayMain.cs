using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class GameplayMain : MonoBehaviour
    {
        private EnemyWavesSpawner _enemyWavesController;

        [Inject]
        private void Construct(EnemyWavesSpawner enemyWavesSpawner)
        {
            _enemyWavesController = enemyWavesSpawner;
        }

        private void Start()
        {
            StartCoroutine(_enemyWavesController.SpawnWave(3));
        }
    }
}