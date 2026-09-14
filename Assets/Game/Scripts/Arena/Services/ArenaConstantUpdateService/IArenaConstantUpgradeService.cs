using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Services.Configs.Upgrades;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.ArenaConstantUpdateService
{
    public interface IArenaConstantUpgradeService
    {
        event Action OnUpgradesChanged;
        IEnumerable<UpgradeSettings> GetUpgrades();
        bool IsAvailable(UpgradeSettings upgrade);
        UpgradeSettings GetUpgrade(string id);
        void BuyUpgrade(UpgradeSettings upgrade);
        int GetLevel(UpgradeSettings upgrade);
        int GetLevelCost(UpgradeSettings upgrade);
        Sprite GetIcon(string upgradeId);
    }
}