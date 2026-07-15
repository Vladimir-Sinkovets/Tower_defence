using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Configs.Upgrades;
using Assets.Game.Scripts.Upgrades.Interfaces;
using UnityEngine;

namespace Assets.Game.Scripts.Upgrades.Implementations
{
    public class BuildingUpgradeApplier : IBuildingUpgradeApplier
    {
        private readonly ISaveService _saveService;
        private readonly UpgradesSettings _upgradesSettings;

        public BuildingUpgradeApplier(GameSettingsService gameSettingsService, ISaveService saveService)
        {
            _upgradesSettings = gameSettingsService.GameSettings.UpgradesSettings;
            _saveService = saveService;
        }
        
        public int ApplyBuildingDamageUpgrade(int baseDamage, BuildingType buildingType)
        {
            UpgradeSettings upgrade = buildingType switch
            {
                BuildingType.Tower => _upgradesSettings.TowerDamageUpgradeSettings,
                BuildingType.Castle => _upgradesSettings.CastleDamageUpgradeSettings,
                
                _ => throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null)
            };
            
            var level = GetUpgradeLevel(upgrade);

            return Mathf.RoundToInt(upgrade.ApplyEffect(level, baseDamage));
        }
        
        public float ApplyBuildingAttackSpeedUpgrade(float baseInterval, BuildingType buildingType)
        {
            UpgradeSettings upgrade = buildingType switch
            {
                BuildingType.Tower => _upgradesSettings.TowerAttackSpeedUpgradeSettings,
                BuildingType.Castle => _upgradesSettings.CastleAttackSpeedUpgradeSettings,
                
                _ => throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null)
            };
            
            var level = GetUpgradeLevel(upgrade);

            return upgrade.ApplyEffect(level, baseInterval);
        }

        public int ApplyCastleHpUpgrade(int baseHp)
        {
            var upgrade = _upgradesSettings.CastleHpUpgradeSettings;
            
            var level = GetUpgradeLevel(upgrade);
            
            return (int) upgrade.ApplyEffect(level, baseHp);
        }
        
        private int GetUpgradeLevel(UpgradeSettings upgrade) => _saveService.Upgrades.GetValueOrDefault(upgrade.Id, _upgradesSettings.UpgradeLevel);
    }
}