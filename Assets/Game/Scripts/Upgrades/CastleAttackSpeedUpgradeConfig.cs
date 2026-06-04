using UnityEngine;

namespace Assets.Game.Scripts.Upgrades
{
    [CreateAssetMenu(menuName = "Configs/Upgrades/Castle attack speed upgrade", fileName = "CastleAttackSpeedUpgradeConfig")]
    public class CastleAttackSpeedUpgradeConfig : UpgradeConfig
    {
        public float MinInterval = 0.3f;

        public override float ApplyEffect(int level, float baseValue)
            => Mathf.Max(baseValue * Mathf.Pow(1f - Upgrade / 100f, level), MinInterval);
    }
}