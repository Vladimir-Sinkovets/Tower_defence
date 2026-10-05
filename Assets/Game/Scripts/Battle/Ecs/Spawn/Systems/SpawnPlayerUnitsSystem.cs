using Assets.Game.Scripts.Battle.Common;
using Assets.Game.Scripts.Battle.Configs;
using Assets.Game.Scripts.Battle.Ecs.CurrencyBank;
using Assets.Game.Scripts.Battle.Ecs.Extensions;
using Assets.Game.Scripts.Battle.Ecs.GameManagement;
using Assets.Game.Scripts.Battle.Ecs.Input;
using Assets.Game.Scripts.Battle.Services.UnitFactories;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Spawn.Systems
{
    public class SpawnPlayerUnitsSystem : ISystem
    {
        private readonly IUnitFactory _factory;
        private readonly BattleSpawnBordersConfig _battleSpawnBordersConfig;
        public World World { get; set; }
        
        private Filter _clickOnFieldEvents;
        private Filter _unitChosenEvents;
        private Filter _currency;
        
        private Stash<ClickOnFieldEvent> _eventStash;
        private Stash<UnitChosenEvent> _unitChosenEventStash;
        private Stash<Currency> _currencyStash;
        private Stash<CurrencyChangedEvent> _currencyChangedEventStash;
        private Stash<GameManager> _gamManagerStash;

        private Entity _gameManagerEntity;
        
        private UnitConfig _config;

        public SpawnPlayerUnitsSystem(IUnitFactory factory, BattleSpawnBordersConfig battleSpawnBordersConfig)
        {
            _factory = factory;
            _battleSpawnBordersConfig = battleSpawnBordersConfig;
        }

        public void OnAwake()
        {
            _unitChosenEvents = World.Filter
                .With<UnitChosenEvent>()
                .Build();
            
            _clickOnFieldEvents = World.Filter
                .With<ClickOnFieldEvent>()
                .Build();

            _currency = World.Filter
                .With<Currency>()
                .Build();
            
            _gameManagerEntity = World.GetManagerEntity();
            
            _unitChosenEventStash = World.GetStash<UnitChosenEvent>();
            _eventStash = World.GetStash<ClickOnFieldEvent>();
            _currencyStash = World.GetStash<Currency>();
            _currencyChangedEventStash = World.GetStash<CurrencyChangedEvent>();
            _gamManagerStash = World.GetStash<GameManager>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_gamManagerStash.Get(_gameManagerEntity).GameEnded)
                return;
            
            var currencyEntity = _currency.First();
            
            ref var currency = ref _currencyStash.Get(currencyEntity);
            
            foreach (var entity in _unitChosenEvents)
            {
                ref var unitChosenEvent = ref _unitChosenEventStash.Get(entity);
                
                _config = unitChosenEvent.Config;
                
                World.RemoveEntity(entity);
            }
            
            foreach (var eventEntity in _clickOnFieldEvents)
            {
                ref var clickEvent = ref _eventStash.Get(eventEntity);
                
                if (_config == null)
                    return;

                if (!_battleSpawnBordersConfig.PlayerSpawnArea.Contains(new Vector2(clickEvent.Position.x, clickEvent.Position.z)))
                    return;
                
                if (currency.Value >= _config.Price)
                {
                    var entity = World.CreateEntity();
                    
                    _factory.CreateUnit(_config, clickEvent.Position, entity, World, TeamIndexes.Player);
                    
                    currency.Value -= _config.Price;
                    
                    _currencyChangedEventStash.Set(World.CreateEntity(), new() { Value = currency.Value } );
                }
            }
        }
        
        public void Dispose() { }
    }
}