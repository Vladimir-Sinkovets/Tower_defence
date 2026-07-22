using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Saves
{
    public interface ISaveService
    {
        event Action OnUpgradesChanged;
        event Action OnMetaCurrencyChanged;
        event Action OnWavesRecordChanged;
        int MetaCurrency { get; set; }
        int WavesRecord { get; set; }
        bool IsAdsDisabled { get; }
        IReadOnlyDictionary<string, int> Upgrades { get; }
        bool TryAddUpgrade(string upgradeId, int firstLevel);
        void SetUpgrade(string upgradeId, int level);
        void Save();
        void SetUpgrades(Dictionary<string, int> saveDataUpgrades);
        void DisableAds();
    }
}