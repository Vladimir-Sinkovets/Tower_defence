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
        private Stash<ClickOnFieldEvent> _eventStash;

        public SpawnPlayerUnitsSystem(IUnitFactory factory)
        {
            _factory = factory;
        }
        
        public void OnAwake()
        {
            _events = World.Filter
                .With<ClickOnFieldEvent>()
                .Build();
            
            _eventStash = World.GetStash<ClickOnFieldEvent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var eventEntity in _events)
            {
                ref var clickEvent = ref _eventStash.Get(eventEntity);

                _factory.CreateUnit(clickEvent.Position);
            }
        }
        
        public void Dispose() { }
    }
}