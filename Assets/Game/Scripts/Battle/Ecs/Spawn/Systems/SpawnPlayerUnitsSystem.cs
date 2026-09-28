using Assets.Game.Scripts.Battle.Configs;
using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Ecs.Input;
using Assets.Game.Scripts.Battle.Services.UnitFactories;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Spawn.Systems
{
    public class SpawnPlayerUnitsSystem : ISystem
    {
        private readonly IUnitFactory _factory;
        public World World { get; set; }
        
        private Filter _events;
        private Filter _unitChosenEvents;
        
        private Stash<ClickOnFieldEvent> _eventStash;
        private Stash<UnitChosenEvent> _unitChosenEventStash;
        private Stash<Team> _playerUnitStash;

        private UnitConfig _config;

        public SpawnPlayerUnitsSystem(IUnitFactory factory) => _factory = factory;

        public void OnAwake()
        {
            _unitChosenEvents = World.Filter
                .With<UnitChosenEvent>()
                .Build();
            
            _events = World.Filter
                .With<ClickOnFieldEvent>()
                .Build();
            
            _unitChosenEventStash = World.GetStash<UnitChosenEvent>();
            _eventStash = World.GetStash<ClickOnFieldEvent>();

            _playerUnitStash = World.GetStash<Team>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _unitChosenEvents)
            {
                ref var unitChosenEvent = ref _unitChosenEventStash.Get(entity);
                
                _config = unitChosenEvent.Config;
                
                World.RemoveEntity(entity);
            }
            
            foreach (var eventEntity in _events)
            {
                ref var clickEvent = ref _eventStash.Get(eventEntity);

                var entity = World.CreateEntity();
                
                if (_config == null)
                    return;
                
                _factory.CreateUnit(_config, clickEvent.Position, entity, World);

                _playerUnitStash.Set(entity, new());
            }
        }
        
        public void Dispose() { }
    }
}