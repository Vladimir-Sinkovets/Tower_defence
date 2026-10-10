using System.Collections.Generic;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Configs.Upgrades;

namespace Assets.Game.Scripts.Battle.Services.UpgradeAppliers
{
    public class UpgradeApplier : IUpgradeApplier
    {
        private readonly GameDataHolder _gameDataHolder;
        private readonly GameSettings _settings;

        public UpgradeApplier(IGameSettingsAccessor settingsAccessor, GameDataHolder gameDataHolder)
        {
            _gameDataHolder = gameDataHolder;
            _settings = settingsAccessor.Settings;
        }
        
        public int ApplyTankDamageUpgrade(int baseDamage)
        {
            var upgradesSettings = _settings.UpgradesSettings;
            
            var upgrade = upgradesSettings.BattleTankDamageUpgradeSettings;
            
            var level = GetUpgradeLevel(upgrade);
            
            return (int) upgrade.ApplyEffect(level, baseDamage);
        }
        
        public int ApplyDefaultUnitDamageUpgrade(int baseDamage)
        {
            var upgradesSettings = _settings.UpgradesSettings;
            
            var upgrade = upgradesSettings.BattleDefaultUnitDamageUpgradeSettings;
            
            var level = GetUpgradeLevel(upgrade);
            
            return (int) upgrade.ApplyEffect(level, baseDamage);
        }

        public int ApplyStartCurrencyUpgrade(int baseCurrency)
        {
            var upgradesSettings = _settings.UpgradesSettings;
            
            var upgrade = upgradesSettings.BattleStartCurrencyUpgradeSettings;
            
            var level = GetUpgradeLevel(upgrade);
            
            return (int) upgrade.ApplyEffect(level, baseCurrency);
        }
        
        private int GetUpgradeLevel(UpgradeSettings upgrade)
        {
            var upgradesSettings = _settings.UpgradesSettings;
            
            return _gameDataHolder.Data.GetUpgradeLevel(upgrade.Id, upgradesSettings.UpgradeLevel);
        }
    }
}