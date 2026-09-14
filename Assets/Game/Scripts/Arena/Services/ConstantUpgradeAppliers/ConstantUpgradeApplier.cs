using System.Collections.Generic;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Configs.Upgrades;

namespace Assets.Game.Scripts.Arena.Services.ConstantUpgradeAppliers
{
    public class ConstantUpgradeApplier : IConstantUpgradeApplier
    {
        private readonly GameSettings _settings;
        private readonly SaveData _saveData;

        public ConstantUpgradeApplier(IGameSettingsAccessor settingsAccessor, ISaveService saveService)
        {
            _saveData = saveService.SaveData;
            _settings = settingsAccessor.Settings;
        }
        
        public int ApplyDamageUpgrade(int baseDamage)
        {
            var upgradesSettings = _settings.UpgradesSettings;
            
            var upgrade = upgradesSettings.ArenaDamageUpgradeSettings;
            
            var level = GetUpgradeLevel(upgrade);
            
            return (int) upgrade.ApplyEffect(level, baseDamage);
        }

        public int ApplyHpUpgrade(int baseHp)
        {
            var upgradesSettings = _settings.UpgradesSettings;
            
            var upgrade = upgradesSettings.ArenaHpUpgradeSettings;
            
            var level = GetUpgradeLevel(upgrade);
            
            return (int) upgrade.ApplyEffect(level, baseHp);
        }
        
        
        private int GetUpgradeLevel(UpgradeSettings upgrade)
        {
            var upgradesSettings = _settings.UpgradesSettings;
            
            return _saveData.Upgrades.GetValueOrDefault(upgrade.Id, upgradesSettings.UpgradeLevel);
        }
    }
}