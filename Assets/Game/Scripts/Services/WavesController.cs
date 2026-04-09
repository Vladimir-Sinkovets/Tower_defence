using Assets.Game.Scripts.Configs;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class WavesController : MonoBehaviour
    {
        private EnemyWavesSpawner _enemyWavesController;
        private GameContext _gameContext;
        private WavesConfig _wavesConfig;

        private int _wavesCount;

        [Inject]
        private void Construct(EnemyWavesSpawner enemyWavesSpawner, WavesConfig wavesConfig, GameContext gameContext)
        {
            _enemyWavesController = enemyWavesSpawner;
            _gameContext = gameContext;
            _wavesConfig = wavesConfig;
        }

        public void StartWaves()
        {
            StartCoroutine(SpawnWaves());
        }

        private IEnumerator SpawnWaves()
        {
            while (true)
            {
                var enemyCount = _wavesConfig.BaseEnemyCount + _wavesCount * _wavesConfig.NewEnemiesPerWave;

                yield return _enemyWavesController.SpawnWave(enemyCount);

                yield return new WaitUntil(() => 
                    _enemyWavesController.IsSpawning == false &&
                    _gameContext.All.Any(x => !x.IsDied) == false);

                yield return new WaitForSeconds(_wavesConfig.IntervalBetweenWaves);

                _wavesCount++;
            }
        }
    }
}