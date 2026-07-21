using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Saves
{
    public interface ISaveService
    {
        event Action OnUpgradesChanged;
        event Action MetaCurrencyChanged;
        event Action WavesRecordChanged;
        int MetaCurrency { get; set; }
        int WavesRecord { get; set; }
        bool IsaAdsDisabled { get; }
        IReadOnlyDictionary<string, int> Upgrades { get; }
        bool TryAddUpgrade(string upgradeId, int firstLevel);
        void SetUpgrade(string upgradeId, int level);
        void Save();
        void SetUpgrades(Dictionary<string, int> saveDataUpgrades);
        void DisableAds();
    }
}