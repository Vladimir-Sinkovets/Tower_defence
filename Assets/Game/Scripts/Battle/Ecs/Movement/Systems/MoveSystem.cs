using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Movement.Systems
{
    public class MoveSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _entities;
        
        private Stash<Position> _positionStash;
        private Stash<MoveDirection> _moveDirectionStash;
        private Stash<Speed> _speedStash;

        public void OnAwake()
        {
            _entities = World.Filter
                .With<Position>()
                .With<Speed>()
                .With<MoveDirection>()
                .Build();

            _positionStash = World.GetStash<Position>();
            _moveDirectionStash = World.GetStash<MoveDirection>();
            _speedStash = World.GetStash<Speed>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _entities)
            {
                ref var position = ref _positionStash.Get(entity);
                ref var moveDirection = ref _moveDirectionStash.Get(entity);
                ref var speed = ref _speedStash.Get(entity);
                
                position.Value += moveDirection.Value * deltaTime * speed.Value;
            }
        }
        
        public void Dispose() { }
    }
}