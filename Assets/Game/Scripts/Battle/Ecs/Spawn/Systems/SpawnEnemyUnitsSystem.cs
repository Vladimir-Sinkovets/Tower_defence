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
        private Stash<EnemyUnit> _enemyUnitStash;
        
        private readonly IUnitFactory _factory;

        public SpawnEnemyUnitsSystem(IUnitFactory factory) => _factory = factory;

        public void OnAwake()
        {
            _spawners = World.Filter
                .With<EnemySpawner>()
                .Build();

            _spawnerStash = World.GetStash<EnemySpawner>();
            _enemyUnitStash = World.GetStash<EnemyUnit>();
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
                        
            _factory.CreateUnit(spawner.Prefab, position, unitEntity, World);
                        
            _enemyUnitStash.Set(unitEntity, new());
        }

        public void Dispose() { }
    }
}