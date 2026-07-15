using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Configs.Upgrades;
using Assets.Game.Scripts.Upgrades.Interfaces;
using Zenject;

namespace Assets.Game.Scripts.Upgrades.Implementations
{
    public class UpgradeService : IUpgradeService, IDisposable, IInitializable
    {
        public event Action OnUpgradesChanged;

        private readonly ISaveService _saveService;
        private readonly GameSettingsService _gameSettingsService;

        public UpgradeService(ISaveService saveService, GameSettingsService gameSettingsService)
        {
            _saveService = saveService;
            _gameSettingsService = gameSettingsService;
        }
        
        public void Initialize() => _saveService.OnUpgradesChanged += OnUpgradesChangedHandler;

        public IEnumerable<UpgradeSettings> Upgrades => _gameSettingsService.GameSettings.UpgradesSettings.GetUpgradeConfigs();

        public int GetLevel(UpgradeSettings upgrade) => _saveService.Upgrades.GetValueOrDefault(upgrade.Id, 0);

        public int GetLevelCost(UpgradeSettings upgrade) => upgrade.GetCostByLevel(GetLevel(upgrade));

        public bool IsAvailable(UpgradeSettings upgrade)
        {
            var cost = GetLevelCost(upgrade);

            return _saveService.MetaCurrency >= cost;
        }

        public void BuyUpgrade(UpgradeSettings upgrade)
        {
            if (upgrade == null)
                return;
            
            var cost = GetLevelCost(upgrade);
            
            _saveService.MetaCurrency -= cost;

            if (!_saveService.TryAddUpgrade(upgrade.Id, _gameSettingsService.GameSettings.UpgradesSettings.FirstLevel))
            {
                var newLevel = _saveService.Upgrades[upgrade.Id] + _gameSettingsService.GameSettings.UpgradesSettings.LevelIncrease;
                _saveService.SetUpgrade(upgrade.Id, newLevel);
            }

            _saveService.Save();
        }

        public UpgradeSettings GetUpgrade(string id) => Upgrades.FirstOrDefault(x => x.Id == id);

        private void OnUpgradesChangedHandler() => OnUpgradesChanged?.Invoke();

        public void Dispose() => _saveService.OnUpgradesChanged -= OnUpgradesChangedHandler;
    }
}