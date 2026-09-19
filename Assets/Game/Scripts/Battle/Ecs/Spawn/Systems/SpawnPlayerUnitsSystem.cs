using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Ecs.Input;
using Assets.Game.Scripts.Battle.Ecs.Movement;
using Assets.Game.Scripts.Battle.Ecs.Unity;
using Assets.Game.Scripts.Battle.Services.UnitFactories;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Spawn.Systems
{
    public class SpawnPlayerUnitsSystem : ISystem
    {
        private readonly IUnitFactory _factory;
        public World World { get; set; }
        
        private Filter _events;
        
        private Stash<ClickOnFieldEvent> _eventStash;
        
        private Stash<PlayerUnit> _playerUnitStash;
        private Stash<Position> _positionStash;
        private Stash<TransformComponent> _transformStash;

        public SpawnPlayerUnitsSystem(IUnitFactory factory) => _factory = factory;

        public void OnAwake()
        {
            _events = World.Filter
                .With<ClickOnFieldEvent>()
                .Build();
            
            _eventStash = World.GetStash<ClickOnFieldEvent>();

            _playerUnitStash = World.GetStash<PlayerUnit>();
            _positionStash = World.GetStash<Position>();
            _transformStash = World.GetStash<TransformComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var eventEntity in _events)
            {
                ref var clickEvent = ref _eventStash.Get(eventEntity);

                var unitEntity = World.CreateEntity();
                
                var gameObject = _factory.CreateUnit(clickEvent.Position);

                _playerUnitStash.Set(unitEntity, new());
                _positionStash.Set(unitEntity, new() { Value = clickEvent.Position });
                _transformStash.Set(unitEntity, new() { Reference = gameObject.transform });
            }
        }
        
        public void Dispose() { }
    }
}