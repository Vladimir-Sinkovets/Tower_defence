using System;
using Assets.Game.Scripts.Configs;
using System.Collections;
using System.Linq;
using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Enemies
{
    public class WavesController : IDisposable
    {
        private EnemyWavesSpawner _enemyWavesController;
        private Registry<Enemy> _enemyRegistry;
        private WavesConfig _wavesConfig;
        private ICoroutineRunner _coroutineRunner;
        
        private Coroutine _wavesCoroutine;

        public int WavesCount { get; private set; }

        [Inject]
        public void Construct(
            EnemyWavesSpawner enemyWavesSpawner,
            WavesConfig wavesConfig,
            Registry<Enemy> enemyRegistry,
            ICoroutineRunner coroutineRunner)
        {
            _enemyWavesController = enemyWavesSpawner;
            _enemyRegistry = enemyRegistry;
            _wavesConfig = wavesConfig;
            _coroutineRunner = coroutineRunner;
        }

        public void StartWaves() => _wavesCoroutine = _coroutineRunner.Run(SpawnWaves());

        private IEnumerator SpawnWaves()
        {
            while (true)
            {
                var enemyCount = _wavesConfig.BaseEnemyCount + WavesCount * _wavesConfig.NewEnemiesPerWave;

                yield return _enemyWavesController.SpawnWave(enemyCount);

                yield return new WaitUntil(() => 
                    _enemyWavesController.IsSpawning == false &&
                    _enemyRegistry.All.Any(x => !x.Health.IsDead) == false);

                yield return new WaitForSeconds(_wavesConfig.IntervalBetweenWaves);

                WavesCount++;
            }
        }

        public void Dispose() => _coroutineRunner.Stop(_wavesCoroutine);
    }
}