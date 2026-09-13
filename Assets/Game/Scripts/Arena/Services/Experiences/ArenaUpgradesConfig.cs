using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.Experiences
{
    [CreateAssetMenu(fileName = "Arena_upgrades_config", menuName = "Arena/Upgrades")]
    public class ArenaUpgradesConfig : ScriptableObject
    {
        public int ExperienceForLevel = 10;
        public List<UpgradeConfig> Upgrades;
    }

    [Serializable]
    public class UpgradeConfig
    {
        public string Name;
        public Sprite Icon;
    }
}