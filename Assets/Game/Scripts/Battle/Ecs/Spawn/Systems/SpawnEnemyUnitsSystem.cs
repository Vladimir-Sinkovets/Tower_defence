using Assets.Game.Scripts.Battle.Common;
using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Services.UnitFactories;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Spawn.Systems
{
    public class SpawnEnemyUnitsSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _spawners;
        
        private Stash<EnemySpawner> _spawnerStash;
        
        private readonly IUnitFactory _factory;
        private Stash<Team> _teamStash;
        private Stash<Reward> _awardStash;

        public SpawnEnemyUnitsSystem(IUnitFactory factory) => _factory = factory;

        public void OnAwake()
        {
            _spawners = World.Filter
                .With<EnemySpawner>()
                .Build();

            _spawnerStash = World.GetStash<EnemySpawner>();
            _teamStash = World.GetStash<Team>();
            _awardStash = World.GetStash<Reward>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _spawners)
            {
                ref var spawner = ref _spawnerStash.Get(entity);

                if (spawner.Time >= spawner.NextWaveTime)
                {
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
                        
            var position = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
                        
            _factory.CreateUnit(spawner.Config, position, unitEntity, World);
                        
            _teamStash.Set(unitEntity, new() { Index = TeamIndexes.Enemy });
            _awardStash.Set(unitEntity, new() { Value = 1 });
        }

        public void Dispose() { }
    }
}