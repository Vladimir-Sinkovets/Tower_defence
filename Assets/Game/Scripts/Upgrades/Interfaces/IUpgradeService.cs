using System.Collections.Generic;

namespace Assets.Game.Scripts.Upgrades.Interfaces
{
    public interface IUpgradeService
    {
        IEnumerable<UpgradeConfig> Upgrades { get; }
        int GetLevel(UpgradeConfig config);
        int GetLevelCost(UpgradeConfig config);
        bool IsAvailable(UpgradeConfig upgrade);
        void BuyUpgrade(UpgradeConfig upgrade);
        UpgradeConfig GetUpgrade(string id);
    }
}