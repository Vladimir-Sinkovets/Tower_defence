using UnityEngine;

namespace Assets.Game.Scripts.Upgrades
{
    [CreateAssetMenu(menuName = "Configs/Upgrades/Castle Damage Upgrade", fileName = "CastleDamageUpgradeConfig")]
    public class CastleDamageUpgradeConfig : UpgradeConfig
    {
        public override float ApplyEffect(int level, float baseValue)
        {
            return baseValue * (1 + Upgrade / 100f * level);
        }
    }
}