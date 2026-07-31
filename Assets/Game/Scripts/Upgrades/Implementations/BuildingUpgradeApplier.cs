using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Configs.Upgrades;
using Assets.Game.Scripts.Upgrades.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Upgrades.Implementations
{
    public class BuildingUpgradeApplier : IBuildingUpgradeApplier
    {
        private readonly ISaveService _saveService;
        private readonly GameSettings _settings;

        public BuildingUpgradeApplier(IGameSettingsAccessor gameSettingsAccessor, ISaveService saveService)
        {
            _settings = gameSettingsAccessor.Settings;
            _saveService = saveService;
            
        }
        
        public int ApplyBuildingDamageUpgrade(int baseDamage, BuildingType buildingType)
        {
            var upgradesSettings = _settings.UpgradesSettings;
            
            UpgradeSettings upgrade = buildingType switch
            {
                BuildingType.Tower => upgradesSettings.TowerDamageUpgradeSettings,
                BuildingType.Castle => upgradesSettings.CastleDamageUpgradeSettings,
                
                _ => throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null)
            };
            
            var level = GetUpgradeLevel(upgrade);

            return Mathf.RoundToInt(upgrade.ApplyEffect(level, baseDamage));
        }
        
        public float ApplyBuildingAttackSpeedUpgrade(float baseInterval, BuildingType buildingType)
        {
            var upgradesSettings = _settings.UpgradesSettings;
            
            UpgradeSettings upgrade = buildingType switch
            {
                BuildingType.Tower => upgradesSettings.TowerAttackSpeedUpgradeSettings,
                BuildingType.Castle => upgradesSettings.CastleAttackSpeedUpgradeSettings,
                
                _ => throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null)
            };
            
            var level = GetUpgradeLevel(upgrade);

            return upgrade.ApplyEffect(level, baseInterval);
        }

        public int ApplyCastleHpUpgrade(int baseHp)
        {
            var upgradesSettings = _settings.UpgradesSettings;
            
            var upgrade = upgradesSettings.CastleHpUpgradeSettings;
            
            var level = GetUpgradeLevel(upgrade);
            
            return (int) upgrade.ApplyEffect(level, baseHp);
        }
        
        private int GetUpgradeLevel(UpgradeSettings upgrade)
        {
            var upgradesSettings = _settings.UpgradesSettings;
            
            return _saveService.Upgrades.GetValueOrDefault(upgrade.Id, upgradesSettings.UpgradeLevel);
        }
    }
}