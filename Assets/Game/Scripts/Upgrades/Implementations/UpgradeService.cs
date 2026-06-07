using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Upgrades.Interfaces;

namespace Assets.Game.Scripts.Upgrades.Implementations
{
    public class UpgradeService : IUpgradeService
    {
        private readonly ISaveService _saveService;
        private readonly UpgradeConfigs _configs;
        
        public UpgradeService(ISaveService saveService, UpgradeConfigs configs)
        {
            _saveService = saveService;
            _configs = configs;
        }

        public IEnumerable<UpgradeConfig> Upgrades => _configs.List;

        public int GetLevel(UpgradeConfig upgrade)
        {
            var data = _saveService.GetSaveData();

            return data.Upgrades.GetValueOrDefault(upgrade.Id, 0);
        }

        public int GetLevelCost(UpgradeConfig upgrade) => upgrade.GetCostByLevel(GetLevel(upgrade));

        public bool IsAvailable(UpgradeConfig upgrade)
        {
            var cost = GetLevelCost(upgrade);

            return _saveService.GetSaveData().MetaCurrency >= cost;
        }

        public void BuyUpgrade(UpgradeConfig upgrade)
        {
            if (upgrade == null)
                return;
            
            var data = _saveService.GetSaveData();
            
            var cost = GetLevelCost(upgrade);
            
            data.MetaCurrency -= cost;

            if (!data.Upgrades.TryAdd(upgrade.Id, 1))
            {
                data.Upgrades[upgrade.Id] += 1;
            }

            _saveService.Save(data);
        }

        public UpgradeConfig GetUpgrade(string id) => Upgrades.FirstOrDefault(x => x.Id == id);
    }
}