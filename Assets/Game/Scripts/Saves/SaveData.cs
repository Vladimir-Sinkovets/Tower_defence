using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Saves
{
    [Serializable]
    public class SaveData
    {
        public Dictionary<string, int> Upgrades = new();
        public int MetaCurrency;
        public int WavesRecord;
        public bool IsAdsDisabled;
        public DateTime LastSaveDate;

        public static SaveData Default => new SaveData
        {
            MetaCurrency = 0,
            WavesRecord = 0,
            IsAdsDisabled = false,
            Upgrades = new Dictionary<string, int>(),
            LastSaveDate = DateTime.MinValue,
        };
    }
}