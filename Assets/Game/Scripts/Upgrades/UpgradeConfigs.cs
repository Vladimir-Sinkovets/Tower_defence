using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "UpgradeConfigs",  menuName = "Configs/Upgrade configs")]
    public class UpgradeConfigs : ScriptableObject
    {
        public List<UpgradeConfig> Configs;
    }

    [Serializable]
    public class UpgradeConfig
    {
        public string Id;
        public Sprite Icon;
    }
}