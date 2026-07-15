using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Services.Configs.Buildings;
using Assets.Game.Scripts.Services.Configs.MetaCurrency;
using Assets.Game.Scripts.Services.Configs.Upgrades;
using Assets.Game.Scripts.Services.Configs.Waves;

namespace Assets.Game.Scripts.Services.Configs
{
    [Serializable]
    public class GameSettings
    {
        public MetaCurrencySettings MetaCurrencySettings;
        public WavesSettings WavesSettings; 
        public CastleSettings CastleSettings;
        public List<BuildingSettings> BuildingSettings;
        public UpgradesSettings UpgradesSettings;
    }
}