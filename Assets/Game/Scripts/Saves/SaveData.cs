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
    }
}