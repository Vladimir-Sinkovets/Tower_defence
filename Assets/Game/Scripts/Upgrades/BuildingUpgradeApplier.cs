using System;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Saves;
using UnityEngine;

namespace Assets.Game.Scripts.Upgrades
{
    public class BuildingUpgradeApplier : IBuildingUpgradeApplier
    {
        private readonly UpgradeConfigs _upgradeConfigs;
        private readonly ISaveService _saveService;

        public BuildingUpgradeApplier(UpgradeConfigs upgradeConfigs, ISaveService saveService)
        {
            _upgradeConfigs = upgradeConfigs;
            _saveService = saveService;
        }
        
        public int ApplyBuildingDamageUpgrade(int baseDamage, BuildingType buildingType)
        {
            UpgradeConfig upgrade = buildingType switch
            {
                BuildingType.Tower => _upgradeConfigs.GetUpgrade<TowerDamageUpgradeConfig>(),
                BuildingType.Castle => _upgradeConfigs.GetUpgrade<CastleDamageUpgradeConfig>(),
                
                _ => throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null)
            };
            
            var level = GetUpgradeLevel(upgrade);

            return Mathf.RoundToInt(upgrade.ApplyEffect(level, baseDamage));
        }
        
        public float ApplyBuildingAttackSpeedUpgrade(float baseInterval, BuildingType buildingType)
        {
            UpgradeConfig upgrade = buildingType switch
            {
                BuildingType.Tower => _upgradeConfigs.GetUpgrade<TowerAttackSpeedUpgradeConfig>(),
                BuildingType.Castle => _upgradeConfigs.GetUpgrade<CastleAttackSpeedUpgradeConfig>(),
                
                _ => throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null)
            };
            
            var level = GetUpgradeLevel(upgrade);

            return upgrade.ApplyEffect(level, baseInterval);
        }

        public int ApplyCastleHpUpgrade(int baseHp)
        {
            var upgrade = _upgradeConfigs.GetUpgrade<CastleHpUpgradeConfig>();
            
            var level = GetUpgradeLevel(upgrade);
            
            return (int) upgrade.ApplyEffect(level, baseHp);
        }
        
        private int GetUpgradeLevel(UpgradeConfig upgrade)
        {
            var data = _saveService.GetSaveData();

            return data.GetUpgradeLevel(upgrade.Id);
        }
    }
}