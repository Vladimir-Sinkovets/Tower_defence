using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Services.Configs.Upgrades
{
    [Serializable]
    public class UpgradesSettings
    {
        public int UpgradeLevel;
        public int LevelIncrease = 1;
        public int FirstLevel = 1;
        
        public CastleAttackSpeedUpgradeSettings CastleAttackSpeedUpgradeSettings;
        public CastleDamageUpgradeSettings CastleDamageUpgradeSettings;
        public CastleHpUpgradeSettings CastleHpUpgradeSettings;
        public TowerAttackSpeedUpgradeSettings TowerAttackSpeedUpgradeSettings;
        public TowerDamageUpgradeSettings TowerDamageUpgradeSettings;
        
        public ArenaHpUpgradeSettings ArenaHpUpgradeSettings;
        public ArenaDamageUpgradeSettings ArenaDamageUpgradeSettings;

        public IEnumerable<UpgradeSettings> GetUpgradeConfigs()
        {
            yield return CastleAttackSpeedUpgradeSettings;
            yield return CastleDamageUpgradeSettings;
            yield return CastleHpUpgradeSettings;
            yield return TowerAttackSpeedUpgradeSettings;
            yield return TowerDamageUpgradeSettings;
        }
        
        public IEnumerable<UpgradeSettings> GetArenaUpgradeConfigs()
        {
            yield return ArenaHpUpgradeSettings;
            yield return ArenaDamageUpgradeSettings;
        }
    }
}