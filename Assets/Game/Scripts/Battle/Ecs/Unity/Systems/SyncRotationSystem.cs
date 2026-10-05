using Assets.Game.Scripts.Battle.Ecs.Movement;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Unity.Systems
{
    public class SyncRotationSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _entities;
        
        private Stash<Rotation> _rotationStash;
        private Stash<TransformComponent> _transformStash;

        public void OnAwake()
        {
            _entities = World.Filter
                .With<Rotation>()
                .With<TransformComponent>()
                .Build();
            
            _rotationStash = World.GetStash<Rotation>();
            _transformStash = World.GetStash<TransformComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _entities)
            {
                ref var rotation = ref _rotationStash.Get(entity);
                ref var transform = ref _transformStash.Get(entity);
                
                transform.Reference.rotation = rotation.Value;
            }
        }
        
        public void Dispose() { }
    }
}