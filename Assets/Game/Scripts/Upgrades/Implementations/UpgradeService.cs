using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Configs.Upgrades;
using Assets.Game.Scripts.Upgrades.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Upgrades.Implementations
{
    public class UpgradeService : IUpgradeService, IDisposable, IInitializable
    {
        public event Action OnUpgradesChanged;

        private readonly UpgradeConfigs _upgradeConfigs;
        private readonly GameSettings _settings;
        private readonly ISaveService _saveService;
        private readonly GameDataHolder _gameDataHolder;

        public UpgradeService(ISaveService saveService,
            GameDataHolder gameDataHolder,
            IGameSettingsAccessor gameSettingsAccessor,
            UpgradeConfigs upgradeConfigs)
        {
            _saveService = saveService;
            _gameDataHolder = gameDataHolder;
            _settings = gameSettingsAccessor.Settings;
            _upgradeConfigs = upgradeConfigs;
        }
        
        public void Initialize() => _gameDataHolder.Data.OnChanged += OnChangedHandler;

        public IEnumerable<UpgradeSettings> GetUpgrades() => _settings.UpgradesSettings.GetUpgradeConfigs();

        public int GetLevel(UpgradeSettings upgrade) => _gameDataHolder.Data.GetUpgradeLevel(upgrade.Id, 0);

        public Sprite GetIcon(string id) => _upgradeConfigs.Configs.FirstOrDefault(x => x.Id == id)?.Icon;
        
        public int GetLevelCost(UpgradeSettings upgrade) => upgrade.GetCostByLevel(GetLevel(upgrade));

        public bool IsAvailable(UpgradeSettings upgrade)
        {
            var cost = GetLevelCost(upgrade);

            return _gameDataHolder.Data.MetaCurrency >= cost;
        }

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

            if (!_gameDataHolder.Data.TryAddUpgrade(upgrade.Id, _settings.UpgradesSettings.FirstLevel))
            {
                var newLevel = _gameDataHolder.Data.GetUpgrade(upgrade.Id) + _settings.UpgradesSettings.LevelIncrease;
                
                _gameDataHolder.Data.SetUpgrade(upgrade.Id, newLevel);
            }

            _saveService.Save();
        }

        public UpgradeSettings GetUpgrade(string id) => GetUpgrades().FirstOrDefault(x => x.Id == id);

        private void OnChangedHandler() => OnUpgradesChanged?.Invoke();

        public void Dispose() => _gameDataHolder.Data.OnChanged -= OnChangedHandler;
    }
}