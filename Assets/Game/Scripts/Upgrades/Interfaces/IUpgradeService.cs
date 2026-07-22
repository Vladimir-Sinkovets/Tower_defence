using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Services.Configs.Upgrades;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Upgrades.Interfaces
{
    public interface IUpgradeService
    {
        event Action OnUpgradesChanged;
        IEnumerable<UpgradeSettings> GetUpgrades();
        int GetLevel(UpgradeSettings settings);
        Sprite GetIcon(string id);
        int GetLevelCost(UpgradeSettings settings);
        bool IsAvailable(UpgradeSettings upgrade);
        void BuyUpgrade(UpgradeSettings upgrade);
        UpgradeSettings GetUpgrade(string id);
    }
}