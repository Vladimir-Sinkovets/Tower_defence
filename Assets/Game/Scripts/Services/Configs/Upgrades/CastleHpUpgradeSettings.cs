using System;

namespace Assets.Game.Scripts.Services.Configs.Upgrades
{
    [Serializable]
    public class CastleHpUpgradeSettings : UpgradeSettings
    {
        public override float ApplyEffect(int level, float baseValue) => baseValue * (1 + Upgrade * level / 100.0f);
    }
}