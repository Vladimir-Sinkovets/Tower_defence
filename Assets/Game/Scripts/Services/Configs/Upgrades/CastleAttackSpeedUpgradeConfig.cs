using System;
using UnityEngine;

namespace Assets.Game.Scripts.Services.Configs.Upgrades
{
    [Serializable]
    public class CastleAttackSpeedUpgradeSettings : UpgradeSettings
    {
        public float MinInterval = 0.3f;

        public override float ApplyEffect(int level, float baseValue)
            => Mathf.Max(baseValue * Mathf.Pow(1f - Upgrade / 100f, level), MinInterval);
    }
}