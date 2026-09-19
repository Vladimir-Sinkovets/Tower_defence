using Assets.Game.Scripts.Battle.Ecs.Movement;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Unity.Systems
{
    public class SyncPositionSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _entities;
        
        private Stash<Position> _positionStash;
        private Stash<TransformComponent> _transformStash;

        public void OnAwake()
        {
            _entities = World.Filter
                .With<Position>()
                .With<TransformComponent>()
                .Build();
            
            _positionStash = World.GetStash<Position>();
            _transformStash = World.GetStash<TransformComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _entities)
            {
                ref var position = ref _positionStash.Get(entity);
                ref var transform = ref _transformStash.Get(entity);
                
                transform.Reference.position = position.Value;
            }
        }
        
        public void Dispose()
        {
        }
    }
}