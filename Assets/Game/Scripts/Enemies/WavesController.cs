using System;
using Assets.Game.Scripts.Configs;
using System.Collections;
using System.Linq;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;
using UnityEngine;

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

        public WavesController(
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

        public void StartWaves(Health target) => _wavesCoroutine = _coroutineRunner.Run(SpawnWaves(target));

        private IEnumerator SpawnWaves(Health target)
        {
            while (true)
            {
                var enemyCount = _wavesConfig.BaseEnemyCount + WavesCount * _wavesConfig.NewEnemiesPerWave;

                yield return _enemyWavesController.SpawnWave(enemyCount, target);

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