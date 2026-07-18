using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Configs.Upgrades;
using Assets.Game.Scripts.Upgrades.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Upgrades.Implementations
{
    public class UpgradeService : IUpgradeService, IDisposable, IInitializable
    {
        public event Action OnUpgradesChanged;

        private readonly ISaveService _saveService;
        private readonly GameSettingsService _gameSettingsService;
        private readonly UpgradeConfigs _upgradeConfigs;

        public UpgradeService(ISaveService saveService, GameSettingsService gameSettingsService, UpgradeConfigs upgradeConfigs)
        {
            _saveService = saveService;
            _gameSettingsService = gameSettingsService;
            _upgradeConfigs = upgradeConfigs;
        }
        
        public void Initialize() => _saveService.OnUpgradesChanged += OnUpgradesChangedHandler;

        public async UniTask<IEnumerable<UpgradeSettings>> GetUpgradesAsync() => (await _gameSettingsService.GetSettingsAsync()).UpgradesSettings.GetUpgradeConfigs();

        public int GetLevel(UpgradeSettings upgrade) => _saveService.Upgrades.GetValueOrDefault(upgrade.Id, 0);

        public Sprite GetIcon(string id) => _upgradeConfigs.Configs.FirstOrDefault(x => x.Id == id)?.Icon;
        
        public int GetLevelCost(UpgradeSettings upgrade) => upgrade.GetCostByLevel(GetLevel(upgrade));

        public bool IsAvailable(UpgradeSettings upgrade)
        {
            var cost = GetLevelCost(upgrade);

            return _saveService.MetaCurrency >= cost;
        }

        public async UniTask BuyUpgradeAsync(UpgradeSettings upgrade)
        {
            if (upgrade == null)
                return;

            var gameSettings = await _gameSettingsService.GetSettingsAsync();
            
            var cost = GetLevelCost(upgrade);
            
            _saveService.MetaCurrency -= cost;

            if (!_saveService.TryAddUpgrade(upgrade.Id, gameSettings.UpgradesSettings.FirstLevel))
            {
                var newLevel = _saveService.Upgrades[upgrade.Id] + gameSettings.UpgradesSettings.LevelIncrease;
                _saveService.SetUpgrade(upgrade.Id, newLevel);
            }

            _saveService.Save();
        }

        public async UniTask<UpgradeSettings> GetUpgrade(string id) => (await GetUpgradesAsync()).FirstOrDefault(x => x.Id == id);

        private void OnUpgradesChangedHandler() => OnUpgradesChanged?.Invoke();

        public void Dispose() => _saveService.OnUpgradesChanged -= OnUpgradesChangedHandler;
    }
}