using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Game.Scripts.Saves
{
    public static class GameDataMapper
    {
        public static GameDataRecord ToRecord(this GameData data)
        {
            if (data == null) return null;

            return new GameDataRecord
            {
                MetaCurrency = data.MetaCurrency,
                WavesRecord = data.WavesRecord,
                IsAdsDisabled = data.IsAdsDisabled,
                LastSaveDate = data.LastSaveDate,
                Upgrades = data.Upgrades
                    .Select(kv => new UpgradeRecord { Key = kv.Key, Value = kv.Value })
                    .ToList()
            };
        }

        public static GameData ToDomain(this GameDataRecord record)
        {
            if (record == null) return GameData.Default;

            var data = new GameData
            {
                MetaCurrency = record.MetaCurrency,
                WavesRecord = record.WavesRecord,
                IsAdsDisabled = record.IsAdsDisabled,
                LastSaveDate = record.LastSaveDate,
                Upgrades = record.Upgrades?.ToDictionary(x => x.Key, x => x.Value)
                           ?? new Dictionary<string, int>()
            };

            return data;
        }
    }
}