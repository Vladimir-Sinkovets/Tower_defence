using Assets.Game.Scripts.Battle.Ecs.Spawn;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Services.EnemySpawnStarters
{
    public class EnemySpawnStarter : IEnemySpawnStarter
    {
        private readonly World _world;
        private readonly EnemySpawnConfig _config;

        public EnemySpawnStarter(World world, EnemySpawnConfig config)
        {
            _world = world;
            _config = config;
        }

        public void Start()
        {
            _world.GetStash<EnemySpawner>().Set(
                _world.CreateEntity(),
                new()
                {
                    Time = 0,
                    NextWaveTime = _config.TimeBetweenSpawn,
                    TimeBetweenWaves = _config.TimeBetweenSpawn,
                    Prefab = _config.EnemyPrefab,
                    EnemyCount = _config.EnemyCount,
                    IncreaseCountPerWave = _config.IncreaseCountPerWave,
                });
        }
    }
}