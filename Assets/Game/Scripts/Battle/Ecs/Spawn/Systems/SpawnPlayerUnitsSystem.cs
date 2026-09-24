using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Ecs.Input;
using Assets.Game.Scripts.Battle.Services.UnitFactories;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Spawn.Systems
{
    public class SpawnPlayerUnitsSystem : ISystem
    {
        private readonly IUnitFactory _factory;
        private readonly PlayerUnitsConfig _config;
        public World World { get; set; }
        
        private Filter _events;
        
        private Stash<ClickOnFieldEvent> _eventStash;
        
        private Stash<PlayerUnit> _playerUnitStash;

        public SpawnPlayerUnitsSystem(IUnitFactory factory, PlayerUnitsConfig config)
        {
            _factory = factory;
            _config = config;
        }

        public void OnAwake()
        {
            _events = World.Filter
                .With<ClickOnFieldEvent>()
                .Build();
            
            _eventStash = World.GetStash<ClickOnFieldEvent>();

            _playerUnitStash = World.GetStash<PlayerUnit>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var eventEntity in _events)
            {
                ref var clickEvent = ref _eventStash.Get(eventEntity);

                var entity = World.CreateEntity();
                
                _factory.CreateUnit(_config.Prefab, clickEvent.Position, entity, World);

                _playerUnitStash.Set(entity, new());
            }
        }
        
        public void Dispose() { }
    }
}