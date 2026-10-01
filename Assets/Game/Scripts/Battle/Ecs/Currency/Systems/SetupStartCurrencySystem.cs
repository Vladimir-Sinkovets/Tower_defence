using Assets.Game.Scripts.Battle.Configs;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.CurrencyBank.Systems
{
    public class SetupStartCurrencySystem : IInitializer
    {
        public World World { get; set; }
        
        private readonly BattleConfig _config;

        public SetupStartCurrencySystem(BattleConfig config) => _config = config;

        public void Dispose() { }

        public void OnAwake()
        {
            World.GetStash<Currency>()
                .Set(World.CreateEntity(), 
                    new()
                    {
                        Value = _config.StartCurrency
                    });
            
            World.GetStash<CurrencyChangedEvent>()
                .Set(World.CreateEntity(), 
                    new CurrencyChangedEvent()
                    {
                        Value = _config.StartCurrency
                    });
        }
    }
}