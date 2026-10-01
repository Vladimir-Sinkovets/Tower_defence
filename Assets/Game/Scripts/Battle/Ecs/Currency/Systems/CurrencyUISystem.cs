using Assets.Game.Scripts.Battle.Ecs.CurrencyBank.Views;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.CurrencyBank.Systems
{
    public class CurrencyUISystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _events;

        private Stash<CurrencyChangedEvent> _currencyChangedEventStash;
        
        private readonly ICurrencyView _currencyView;

        public CurrencyUISystem(ICurrencyView currencyView) => _currencyView = currencyView;

        public void OnAwake()
        {
            _events = World.Filter
                .With<CurrencyChangedEvent>()
                .Build();

            _currencyChangedEventStash = World.GetStash<CurrencyChangedEvent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _events)
            {
                ref var currencyChangedEvent = ref _currencyChangedEventStash.Get(entity);
                
                _currencyView.SetCurrency(currencyChangedEvent.Value);
            }
        }
        
        public void Dispose() { }
    }
}