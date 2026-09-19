using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Ecs.Movement;
using Assets.Game.Scripts.Battle.Services.UnitFactories;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Test
{
    public class InitializeTestEnemy : IInitializer
    {
        public World World { get; set; }
        
        private readonly IUnitFactory _factory;

        public InitializeTestEnemy(IUnitFactory factory) => _factory = factory;

        public void OnAwake()
        {
            var enemyStash = World.GetStash<EnemyUnit>();
            var positionStash = World.GetStash<Position>();
            
            
            var unit = _factory.CreateUnit(Vector3.zero);

            var renderers = unit.GetComponents<Renderer>();

            foreach (var renderer in renderers)
            {
                renderer.material.color = Color.red;
            }
            
            var entity = World.CreateEntity();
            
            enemyStash.Set(entity, new());
            positionStash.Set(entity, new() { Value = unit.transform.position });
            
            Debug.Log($"Entity: {entity.Id}, IsDisposed: {entity.IsDisposed()}");
            Debug.Log($"Has EnemyUnit: {entity.Has<EnemyUnit>()}");
        }

        public void Dispose() { }
    }
}