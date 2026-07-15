using System;

namespace Assets.Game.Scripts.Services.Configs.Upgrades
{
    [Serializable]
    public class TowerDamageUpgradeSettings : UpgradeSettings
    {
        public override float ApplyEffect(int level, float baseValue)
        {
            return baseValue * (1 + Upgrade / 100f * level);
        }
    }
}