using System;

namespace Assets.Game.Scripts.Services.Configs.Upgrades
{
    [Serializable]
    public class ArenaHpUpgradeSettings : UpgradeSettings
    {
        public override float ApplyEffect(int level, float baseValue) => baseValue * (1 + Upgrade / 100f * level);
    }
}