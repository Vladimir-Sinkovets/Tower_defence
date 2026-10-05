using System;

namespace Assets.Game.Scripts.Services.Configs.Upgrades
{
    [Serializable]
    public class BattleTankDamageUpgradeSettings :  UpgradeSettings
    {
        public override float ApplyEffect(int level, float baseValue) => baseValue + (level * Upgrade);
    }
}