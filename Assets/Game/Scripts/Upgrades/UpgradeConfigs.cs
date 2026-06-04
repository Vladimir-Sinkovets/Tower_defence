using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Game.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "UpgradeConfigs",  menuName = "Configs/Upgrade configs")]
    public class UpgradeConfigs : ScriptableObject
    {
        public List<UpgradeConfig> List;
        
        public T GetUpgrade<T>() where T : UpgradeConfig
        {
            return List.FirstOrDefault(upgradeConfig => upgradeConfig is T) as T;
        }
    }
}