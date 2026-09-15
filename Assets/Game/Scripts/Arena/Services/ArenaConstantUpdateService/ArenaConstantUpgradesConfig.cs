using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.ArenaConstantUpdateService
{
    [CreateAssetMenu(fileName = "Arena_constant_upgrades_config", menuName = "Arena/Constant upgrades Config")]
    public class ArenaConstantUpgradesConfig : ScriptableObject
    {
        public List<ArenaConstantUpgradeConfig> Configs;
    }

    [Serializable]
    public class ArenaConstantUpgradeConfig
    {
        public string Id;
        public Sprite Icon;
    }
}
