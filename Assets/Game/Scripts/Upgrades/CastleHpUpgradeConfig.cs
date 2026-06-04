using UnityEngine;

namespace Assets.Game.Scripts.Upgrades
{
    [CreateAssetMenu(menuName = "Configs/Upgrades/Castle Hp Upgrade Config", fileName = "CastleHpUpgradeConfig")]
    public class CastleHpUpgradeConfig : UpgradeConfig
    {
        public override float ApplyEffect(int level, float baseValue) => baseValue * (1 + Upgrade * level / 100.0f);
    }
}