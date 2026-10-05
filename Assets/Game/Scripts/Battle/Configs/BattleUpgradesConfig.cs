using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Configs
{
    [CreateAssetMenu(fileName = "Battle_upgrades_config", menuName = "Battle/Upgrades config")]
    public class BattleUpgradesConfig : ScriptableObject
    {
        public List<BattleUpgradeConfig> Configs;
    }

    [Serializable]
    public class BattleUpgradeConfig
    {
        public string Id;
        public Sprite Icon;
    }
}