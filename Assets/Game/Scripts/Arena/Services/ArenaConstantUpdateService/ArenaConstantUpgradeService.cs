using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Configs.Upgrades;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.ArenaConstantUpdateService
{
    public class ArenaConstantUpgradeService : IArenaConstantUpgradeService, IInitializable
    {
        private readonly ISaveService _saveService;
        private readonly ArenaConstantUpgradesConfig _config;
        private readonly GameSettings _settings;
        private readonly SaveData _saveData;

        public event Action OnUpgradesChanged;

        public ArenaConstantUpgradeService(ISaveService saveService, ArenaConstantUpgradesConfig config, IGameSettingsAccessor gameSettingsAccessor)
        {
            _saveService = saveService;
            _saveData = saveService.SaveData;
            
            _config = config;
            _settings = gameSettingsAccessor.Settings;
        }
        
        public void Initialize() => _saveData.OnChanged += OnChangedHandler;
        
        public IEnumerable<UpgradeSettings> GetUpgrades() => _settings.UpgradesSettings.GetArenaUpgradeConfigs();

        public bool IsAvailable(UpgradeSettings upgrade)
        {
            var cost = GetLevelCost(upgrade);

            return _saveData.MetaCurrency >= cost;
        }

        public UpgradeSettings GetUpgrade(string id) => GetUpgrades().FirstOrDefault(x => x.Id == id);

        public void BuyUpgrade(UpgradeSettings upgrade)
        {
            if (upgrade == null)
                return;
            
            var cost = GetLevelCost(upgrade);

            if (_saveData.MetaCurrency < cost)
            {
                Debug.LogError($"Player does not have enough currency to buy the upgrade ({upgrade.Id})");
                return;
            }
            
            _saveData.MetaCurrency -= cost;

            if (!_saveData.Upgrades.TryAdd(upgrade.Id, _settings.UpgradesSettings.FirstLevel))
            {
                var newLevel = _saveData.Upgrades[upgrade.Id] + _settings.UpgradesSettings.LevelIncrease;
                
                _saveData.Upgrades[upgrade.Id] = newLevel;
            }

            _saveService.Save();
        }
        
        public int GetLevelCost(UpgradeSettings upgrade) => upgrade.GetCostByLevel(GetLevel(upgrade));
        
        public Sprite GetIcon(string id) => _config.Configs.FirstOrDefault(x => x.Id == id)?.Icon;

        public int GetLevel(UpgradeSettings upgrade) => _saveData.Upgrades.GetValueOrDefault(upgrade.Id, 0);
     
        
        private void OnChangedHandler() => OnUpgradesChanged?.Invoke();
    }
}