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
        private readonly GameDataHolder _gameDataHolder;
        private readonly ArenaConstantUpgradesConfig _config;
        private readonly GameSettings _settings;

        public event Action OnUpgradesChanged;

        public ArenaConstantUpgradeService(
            ISaveService saveService,
            GameDataHolder gameDataHolder,
            ArenaConstantUpgradesConfig config,
            IGameSettingsAccessor gameSettingsAccessor)
        {
            _saveService = saveService;
            _gameDataHolder = gameDataHolder;

            _config = config;
            _settings = gameSettingsAccessor.Settings;
        }
        
        public void Initialize() => _gameDataHolder.Data.OnChanged += OnChangedHandler;
        
        public IEnumerable<UpgradeSettings> GetUpgrades() => _settings.UpgradesSettings.GetArenaUpgradeConfigs();

        public bool IsAvailable(UpgradeSettings upgrade)
        {
            var cost = GetLevelCost(upgrade);

            return _gameDataHolder.Data.MetaCurrency >= cost;
        }

        public UpgradeSettings GetUpgrade(string id) => GetUpgrades().FirstOrDefault(x => x.Id == id);

        public void BuyUpgrade(UpgradeSettings upgrade)
        {
            if (upgrade == null)
                return;
            
            var cost = GetLevelCost(upgrade);

            if (_gameDataHolder.Data.MetaCurrency < cost)
            {
                Debug.LogError($"Player does not have enough currency to buy the upgrade ({upgrade.Id})");
                return;
            }
            
            _gameDataHolder.Data.MetaCurrency -= cost;

            if (!_gameDataHolder.Data.Upgrades.TryAdd(upgrade.Id, _settings.UpgradesSettings.FirstLevel))
            {
                var newLevel = _gameDataHolder.Data.Upgrades[upgrade.Id] + _settings.UpgradesSettings.LevelIncrease;
                
                _gameDataHolder.Data.Upgrades[upgrade.Id] = newLevel;
            }

            _saveService.Save();
        }
        
        public int GetLevelCost(UpgradeSettings upgrade) => upgrade.GetCostByLevel(GetLevel(upgrade));
        
        public Sprite GetIcon(string id) => _config.Configs.FirstOrDefault(x => x.Id == id)?.Icon;

        public int GetLevel(UpgradeSettings upgrade) => _gameDataHolder.Data.Upgrades.GetValueOrDefault(upgrade.Id, 0);
     
        
        private void OnChangedHandler() => OnUpgradesChanged?.Invoke();
    }
}