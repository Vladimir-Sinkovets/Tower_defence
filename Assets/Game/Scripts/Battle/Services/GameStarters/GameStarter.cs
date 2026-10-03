using Assets.Game.Scripts.Battle.Ecs.Extensions;
using Assets.Game.Scripts.Battle.Ecs.GameManagement;
using Assets.Game.Scripts.Battle.Ecs.Spawn;
using Assets.Game.Scripts.Battle.Services.WorldAccessors;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Services.GameStarters
{
    public class GameStarter : IGameStarter
    {
        private readonly World _world;
        private readonly EnemySpawnConfig _config;

        public GameStarter(IWorldAccessor worldAccessor, EnemySpawnConfig config)
        {
            _world = worldAccessor.World;
            _config = config;
        }

        public void Start()
        {
            ref var gameManager = ref _world.GetStash<GameManager>().Get(_world.GetManagerEntity());
            
            gameManager.GameStarted = true;
            
            _world.GetStash<EnemySpawner>().Set(
                _world.CreateEntity(),
                new()
                {
                    Time = 0,
                    NextWaveTime = _config.TimeBetweenSpawn,
                    TimeBetweenWaves = _config.TimeBetweenSpawn,
                    Config = _config.EnemyConfig,
                    EnemyCount = _config.EnemyCount,
                    IncreaseCountPerWave = _config.IncreaseCountPerWave,
                });
        }
    }
}