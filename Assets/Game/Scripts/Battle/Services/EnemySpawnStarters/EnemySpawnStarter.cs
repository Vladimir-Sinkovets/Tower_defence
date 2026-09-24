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
                    NextSpawnTime = _config.TimeBetweenSpawn,
                    TimeBetweenSpawn = _config.TimeBetweenSpawn,
                    Prefab = _config.EnemyPrefab,
                });
        }
    }
}