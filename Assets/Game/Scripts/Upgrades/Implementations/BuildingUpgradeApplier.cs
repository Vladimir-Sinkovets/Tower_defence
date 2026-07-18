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
        private readonly GameSettingsService _gameSettingsService;
        private readonly ISaveService _saveService;

        public BuildingUpgradeApplier(GameSettingsService gameSettingsService, ISaveService saveService)
        {
            _gameSettingsService = gameSettingsService;
            _saveService = saveService;
        }
        
        public async UniTask<int> ApplyBuildingDamageUpgradeAsync(int baseDamage, BuildingType buildingType)
        {
            var upgradesSettings = (await _gameSettingsService.GetSettingsAsync()).UpgradesSettings;
            
            UpgradeSettings upgrade = buildingType switch
            {
                BuildingType.Tower => upgradesSettings.TowerDamageUpgradeSettings,
                BuildingType.Castle => upgradesSettings.CastleDamageUpgradeSettings,
                
                _ => throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null)
            };
            
            var level = await GetUpgradeLevelAsync(upgrade);

            return Mathf.RoundToInt(upgrade.ApplyEffect(level, baseDamage));
        }
        
        public async UniTask<float> ApplyBuildingAttackSpeedUpgradeAsync(float baseInterval, BuildingType buildingType)
        {
            var upgradesSettings = (await _gameSettingsService.GetSettingsAsync()).UpgradesSettings;
            
            UpgradeSettings upgrade = buildingType switch
            {
                BuildingType.Tower => upgradesSettings.TowerAttackSpeedUpgradeSettings,
                BuildingType.Castle => upgradesSettings.CastleAttackSpeedUpgradeSettings,
                
                _ => throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null)
            };
            
            var level = await GetUpgradeLevelAsync(upgrade);

            return upgrade.ApplyEffect(level, baseInterval);
        }

        public async UniTask<int> ApplyCastleHpUpgradeAsync(int baseHp)
        {
            var upgradesSettings = (await _gameSettingsService.GetSettingsAsync()).UpgradesSettings;
            
            var upgrade = upgradesSettings.CastleHpUpgradeSettings;
            
            var level = await GetUpgradeLevelAsync(upgrade);
            
            return (int) upgrade.ApplyEffect(level, baseHp);
        }
        
        private async UniTask<int> GetUpgradeLevelAsync(UpgradeSettings upgrade)
        {
            var upgradesSettings = (await _gameSettingsService.GetSettingsAsync()).UpgradesSettings;
            
            return _saveService.Upgrades.GetValueOrDefault(upgrade.Id, upgradesSettings.UpgradeLevel);
        }
    }
}