using UnityEngine;

namespace Assets.Game.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "UpgradeConfigs",  menuName = "Configs/Upgrade configs")]
    public class UpgradeConfigs : ScriptableObject
    {
        public Sprite CastleAttackSpeedUpgradeIcon;
        public Sprite CastleDamageUpgradeIcon;
        public Sprite CastleHpUpgradeIcon;
        public Sprite TowerAttackSpeedUpgradeIcon;
        public Sprite TowerDamageUpgradeIcon;
    }
}