using UnityEngine;

namespace Assets.Game.Scripts.Upgrades
{
    [CreateAssetMenu(menuName = "Configs/Upgrades/Tower Damage Upgrade", fileName = "TowerDamageUpgradeConfig")]
    public class TowerDamageUpgradeConfig : UpgradeConfig
    {
        public override float ApplyEffect(int level, float baseValue)
        {
            return baseValue * (1 + Upgrade / 100f * level);
        }
    }
}