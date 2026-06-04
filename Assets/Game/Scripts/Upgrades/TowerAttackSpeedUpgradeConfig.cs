using UnityEngine;

namespace Assets.Game.Scripts.Upgrades
{
    [CreateAssetMenu(menuName = "Configs/Upgrades/Tower Attack Speed Upgrade", fileName = "TowerAttackSpeedUpgradeConfig")]
    public class TowerAttackSpeedUpgradeConfig : UpgradeConfig
    {
        public float MinInterval = 0.3f;

        public override float ApplyEffect(int level, float baseValue)
            => Mathf.Max(baseValue * Mathf.Pow(1f - Upgrade / 100f, level), MinInterval);
    }
}