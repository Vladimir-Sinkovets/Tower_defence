using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Upgrades.Interfaces;

namespace Assets.Game.Scripts.Upgrades.Implementations
{
    public class UpgradeService : IUpgradeService, IDisposable
    {
        public event Action OnUpgradesChanged;

        private readonly ISaveService _saveService;
        private readonly SaveData _saveData;
        private readonly UpgradeConfigs _configs;

        public UpgradeService(ISaveService saveService, SaveData saveData, UpgradeConfigs configs)
        {
            _saveService = saveService;
            _saveData = saveData;
            _configs = configs;

            _saveData.OnUpgradesChanged += OnUpgradesChangedHandler;
        }

        public IEnumerable<UpgradeConfig> Upgrades => _configs.List;

        public int GetLevel(UpgradeConfig upgrade) => _saveData.Upgrades.GetValueOrDefault(upgrade.Id, 0);

        public int GetLevelCost(UpgradeConfig upgrade) => upgrade.GetCostByLevel(GetLevel(upgrade));

        public bool IsAvailable(UpgradeConfig upgrade)
        {
            var cost = GetLevelCost(upgrade);

            return _saveData.MetaCurrency >= cost;
        }

        public void BuyUpgrade(UpgradeConfig upgrade)
        {
            if (upgrade == null)
                return;
            
            var cost = GetLevelCost(upgrade);
            
            _saveData.MetaCurrency -= cost;

            if (!_saveData.Upgrades.TryAdd(upgrade.Id, UpgradeConstants.FirstLevel))
            {
                _saveData.Upgrades[upgrade.Id] += UpgradeConstants.LevelIncrease;
            }

            _saveService.Save();
        }

        public UpgradeConfig GetUpgrade(string id) => Upgrades.FirstOrDefault(x => x.Id == id);

        private void OnUpgradesChangedHandler() => OnUpgradesChanged?.Invoke();

        public void Dispose() => _saveData.OnUpgradesChanged -= OnUpgradesChangedHandler;
    }
}