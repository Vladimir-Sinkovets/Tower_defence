using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Upgrades.Interfaces;
using Zenject;

namespace Assets.Game.Scripts.Upgrades.Implementations
{
    public class UpgradeService : IUpgradeService, IDisposable, IInitializable
    {
        public event Action OnUpgradesChanged;

        private readonly ISaveService _saveService;
        private readonly UpgradeConfigs _configs;

        public UpgradeService(ISaveService saveService, UpgradeConfigs configs)
        {
            _saveService = saveService;
            _configs = configs;
        }
        
        public void Initialize() => _saveService.OnUpgradesChanged += OnUpgradesChangedHandler;

        public IEnumerable<UpgradeConfig> Upgrades => _configs.List;

        public int GetLevel(UpgradeConfig upgrade) => _saveService.Upgrades.GetValueOrDefault(upgrade.Id, 0);

        public int GetLevelCost(UpgradeConfig upgrade) => upgrade.GetCostByLevel(GetLevel(upgrade));

        public bool IsAvailable(UpgradeConfig upgrade)
        {
            var cost = GetLevelCost(upgrade);

            return _saveService.MetaCurrency >= cost;
        }

        public void BuyUpgrade(UpgradeConfig upgrade)
        {
            if (upgrade == null)
                return;
            
            var cost = GetLevelCost(upgrade);
            
            _saveService.MetaCurrency -= cost;

            if (!_saveService.TryAddUpgrade(upgrade.Id, UpgradeConstants.FirstLevel))
            {
                var newLevel = _saveService.Upgrades[upgrade.Id] + UpgradeConstants.LevelIncrease;
                _saveService.SetUpgrade(upgrade.Id, newLevel);
            }

            _saveService.Save();
        }

        public UpgradeConfig GetUpgrade(string id) => Upgrades.FirstOrDefault(x => x.Id == id);

        private void OnUpgradesChangedHandler() => OnUpgradesChanged?.Invoke();

        public void Dispose() => _saveService.OnUpgradesChanged -= OnUpgradesChangedHandler;
    }
}