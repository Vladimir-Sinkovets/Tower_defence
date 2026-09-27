using Assets.Game.Scripts.Battle.Common;
using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Ecs.Attacks;
using Assets.Game.Scripts.Battle.Ecs.Spawn;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Currency.Systems
{
    public class CurrencySystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _events;
        
        private Stash<DeathEvent> _eventStash;
        private Stash<Currency> _currencyStash;
        private Stash<CurrencyChangedEvent> _currencyChangedEventStash;
        private Stash<Reward> _rewardStash;
        private Stash<Team> _teamStash;
        
        private Entity _currencyEntity;

        public void OnAwake()
        {
            _events = World.Filter
                .With<DeathEvent>()
                .Build();

            _eventStash = World.GetStash<DeathEvent>();
            _currencyStash = World.GetStash<Currency>();
            _currencyChangedEventStash = World.GetStash<CurrencyChangedEvent>();
            _rewardStash = World.GetStash<Reward>();
            _teamStash = World.GetStash<Team>();
            
            _currencyEntity = World.CreateEntity();
            _currencyStash.Add(_currencyEntity, new() { Value = 0 });
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _events)
            {
                ref var deathEvent = ref _eventStash.Get(entity);

                if (!World.Has(deathEvent.Target))
                    continue;

                if (!_rewardStash.Has(deathEvent.Target))
                    continue;

                if (!_teamStash.Has(deathEvent.Target))
                    continue;
                
                ref var team = ref _teamStash.Get(deathEvent.Target);
                if (team.Index == TeamIndexes.Player)
                    continue;
                
                ref var reward = ref _rewardStash.Get(deathEvent.Target);
                ref var currency = ref _currencyStash.Get(_currencyEntity);
                
                currency.Value += reward.Value;

                _currencyChangedEventStash.Set(World.CreateEntity(), new() { Value = currency.Value });
            }
        }
        
        public void Dispose() { }
    }
}