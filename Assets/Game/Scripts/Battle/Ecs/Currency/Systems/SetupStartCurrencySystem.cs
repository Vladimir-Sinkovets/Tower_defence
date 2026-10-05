using Assets.Game.Scripts.Battle.Configs;
using Assets.Game.Scripts.Battle.Services.UpgradeAppliers;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.CurrencyBank.Systems
{
    public class SetupStartCurrencySystem : IInitializer
    {
        public World World { get; set; }
        
        private readonly BattleConfig _config;
        private readonly IUpgradeApplier _upgradeApplier;

        public SetupStartCurrencySystem(BattleConfig config, IUpgradeApplier upgradeApplier)
        {
            _config = config;
            _upgradeApplier = upgradeApplier;
        }

        public void Dispose() { }

        public void OnAwake()
        {
            var currency = _upgradeApplier.ApplyStartCurrencyUpgrade(_config.StartCurrency);
            
            World.GetStash<Currency>()
                .Set(World.CreateEntity(), 
                    new()
                    {
                        Value = currency
                    });
            
            World.GetStash<CurrencyChangedEvent>()
                .Set(World.CreateEntity(), 
                    new CurrencyChangedEvent()
                    {
                        Value = currency
                    });
        }
    }
}