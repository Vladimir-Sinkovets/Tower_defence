using System.Collections.Generic;

namespace Assets.Game.Scripts.Saves
{
    public class SaveData
    {
        public int MetaCurrency;
        public int WavesRecord;
        public Dictionary<string, int> Upgrades = new Dictionary<string, int>();

        public int GetUpgradeLevel(string upgradeId) => Upgrades.GetValueOrDefault(upgradeId, 0);
    }
}