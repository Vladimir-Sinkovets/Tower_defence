using UnityEngine;

namespace Assets.Game.Scripts.Upgrades
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        public string Id;
        public string Name;
        public int BaseCost;
        public int CostIncrementPerLevel;
        public float Upgrade;
        public Sprite Icon;
        public string Unit;
        
        public int GetCostByLevel(int level) => BaseCost + CostIncrementPerLevel * level;
        public abstract float ApplyEffect(int level, float baseValue);
    }
}