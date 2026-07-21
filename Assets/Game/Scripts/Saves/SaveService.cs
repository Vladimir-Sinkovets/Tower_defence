using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.Saves
{
    public class SaveService : ISaveService
    {
        public event Action OnUpgradesChanged;
        public event Action MetaCurrencyChanged;
        public event Action WavesRecordChanged;
        
        private readonly SaveData _saveData = new();

        public int MetaCurrency
        {
            get => _saveData.MetaCurrency;
            set
            {
                _saveData.MetaCurrency = value;
                MetaCurrencyChanged?.Invoke();
            }
        }

        public int WavesRecord
        {
            get => _saveData.WavesRecord;
            set
            {
                _saveData.WavesRecord = value;
                WavesRecordChanged?.Invoke();
            }
        }
        
        public bool IsaAdsDisabled => _saveData.IsaAdsDisabled;

        public IReadOnlyDictionary<string, int> Upgrades => _saveData.Upgrades;
        
        public bool TryAddUpgrade(string upgradeId, int level)
        {
            if (!_saveData.Upgrades.TryAdd(upgradeId, level)) return false;
            
            OnUpgradesChanged?.Invoke();
            
            return true;
        }

        public void SetUpgrade(string upgradeId, int level)
        {
            _saveData.Upgrades[upgradeId] = level;

            OnUpgradesChanged?.Invoke();
        }

        public void SetUpgrades(Dictionary<string, int> saveDataUpgrades)
        {
            _saveData.Upgrades = saveDataUpgrades;
            
            OnUpgradesChanged?.Invoke();
        }

        public void DisableAds() => _saveData.IsaAdsDisabled = true;

        public void Save()
        {
            var json = JsonConvert.SerializeObject(_saveData);
            
            PlayerPrefs.SetString(SaveConstants.PlayerPrefsKey, json);
        }
    }
}