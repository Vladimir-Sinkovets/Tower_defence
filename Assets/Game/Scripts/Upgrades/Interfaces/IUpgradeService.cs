using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Services.Configs.Upgrades;

namespace Assets.Game.Scripts.Upgrades.Interfaces
{
    public interface IUpgradeService
    {
        event Action OnUpgradesChanged;
        IEnumerable<UpgradeSettings> Upgrades { get; }
        int GetLevel(UpgradeSettings settings);
        int GetLevelCost(UpgradeSettings settings);
        bool IsAvailable(UpgradeSettings upgrade);
        void BuyUpgrade(UpgradeSettings upgrade);
        UpgradeSettings GetUpgrade(string id);
    }
}