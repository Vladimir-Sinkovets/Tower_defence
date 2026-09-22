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
            
            var entity = World.CreateEntity();
            
            var unit = _factory.CreateUnit(Vector3.zero, entity, World);
            
            enemyStash.Set(entity, new());

            var renderers = unit.GetComponents<Renderer>();

            foreach (var renderer in renderers)
                renderer.material.color = Color.red;
        }

        public void Dispose() { }
    }
}