using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Saves
{
    [Serializable]
    public class GameDataRecord
    {
        public int MetaCurrency;
        public int WavesRecord;
        public bool IsAdsDisabled;
        public DateTime LastSaveDate;
        public List<UpgradeRecord> Upgrades = new();
    }

    [Serializable]
    public class UpgradeRecord
    {
        public string Key;
        public int Value;
    }
}