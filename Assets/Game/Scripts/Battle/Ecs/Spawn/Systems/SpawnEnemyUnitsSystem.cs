using Assets.Game.Scripts.Battle.Common;
using Assets.Game.Scripts.Battle.Configs;
using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Ecs.Extensions;
using Assets.Game.Scripts.Battle.Ecs.GameManagement;
using Assets.Game.Scripts.Battle.Services.UnitFactories;
using Assets.Game.Scripts.Common.Extensions;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Spawn.Systems
{
    public class SpawnEnemyUnitsSystem : ISystem
    {
        public World World { get; set; }
        
        private readonly IUnitFactory _factory;
        private readonly BattleSpawnBordersConfig _battleSpawnBordersConfig;

        private Filter _spawners;
        
        private Stash<EnemySpawner> _spawnerStash;
        private Stash<Team> _teamStash;
        private Stash<Reward> _awardStash;
        private Stash<GameManager> _gameManagerStash;
        
        private Entity _gameManagerEntity;

        public SpawnEnemyUnitsSystem(IUnitFactory factory, BattleSpawnBordersConfig battleSpawnBordersConfig)
        {
            _factory = factory;
            _battleSpawnBordersConfig = battleSpawnBordersConfig;
        }

        public void OnAwake()
        {
            _spawners = World.Filter
                .With<EnemySpawner>()
                .Build();

            _gameManagerEntity = World.GetManagerEntity();

            _spawnerStash = World.GetStash<EnemySpawner>();
            _teamStash = World.GetStash<Team>();
            _awardStash = World.GetStash<Reward>();
            _gameManagerStash = World.GetStash<GameManager>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_gameManagerStash.Get(_gameManagerEntity).GameEnded)
                return;
            
            foreach (var entity in _spawners)
            {
                ref var spawner = ref _spawnerStash.Get(entity);

                if (spawner.Time >= spawner.NextWaveTime)
                {
                    spawner.WaveCount++;

                    for (var i = 0; i < spawner.EnemyCount; i++)
                    {
                        CreateEnemy(spawner);
                    }
                    
                    spawner.NextWaveTime = spawner.Time + spawner.TimeBetweenWaves;
                    
                    spawner.EnemyCount += spawner.IncreaseCountPerWave;
                }
                
                spawner.Time += deltaTime;
            }
        }

        private void CreateEnemy(EnemySpawner spawner)
        {
            var unitEntity = World.CreateEntity();

            var point = _battleSpawnBordersConfig.EnemySpawnArea.GetRandomPointInRect();
            
            var position = new Vector3(point.x, 0, point.y);
                        
            _factory.CreateUnit(spawner.Config, position, unitEntity, World, TeamIndexes.Enemy);
                        
            _teamStash.Set(unitEntity, new() { Index = TeamIndexes.Enemy });
            _awardStash.Set(unitEntity, new() { Value = 1 });
        }

        public void Dispose() { }
    }
}