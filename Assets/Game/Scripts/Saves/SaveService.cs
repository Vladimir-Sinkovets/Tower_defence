using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Services.CloudSaves;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.CloudSave;
using UnityEngine;

namespace Assets.Game.Scripts.Saves
{
    public class SaveService : ISaveService
    {
        private readonly ICloudService _cloudSaveService;
        public event Action OnUpgradesChanged;
        public event Action OnMetaCurrencyChanged;
        public event Action OnWavesRecordChanged;
        
        private SaveData _saveData = new();

        public SaveService(ICloudService cloudSaveService) => _cloudSaveService = cloudSaveService;

        public int MetaCurrency
        {
            get => _saveData.MetaCurrency;
            set
            {
                _saveData.MetaCurrency = value;
                OnMetaCurrencyChanged?.Invoke();
            }
        }

        public int WavesRecord
        {
            get => _saveData.WavesRecord;
            set
            {
                _saveData.WavesRecord = value;
                OnWavesRecordChanged?.Invoke();
            }
        }
        
        public bool IsAdsDisabled => _saveData.IsAdsDisabled;

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

        public void DisableAds() => _saveData.IsAdsDisabled = true;

        public void Save()
        {
            _saveData.LastSaveDate = DateTime.UtcNow;
            
            var json = JsonConvert.SerializeObject(_saveData);
            
            _cloudSaveService.SaveAsync(json);
            
            PlayerPrefs.SetString(SaveConstants.PlayerPrefsKey, json);
        }

        public async UniTask LoadAsync()
        {
            var localData = LoadLocalData();
            var cloudData = await LoadCloudDataAsync();

            if (localData.LastSaveDate > cloudData.LastSaveDate)
            {
                _saveData = localData;
                Debug.Log($"[{nameof(SaveService)}] Loaded local data");
            }
            else
            {
                _saveData = cloudData;
                Debug.Log($"[{nameof(SaveService)}] Loaded cloud data");
            }
        }

        private async UniTask<SaveData> LoadCloudDataAsync()
        {
            var cloudJson = await _cloudSaveService.LoadAsync();

            return CreateSaveData(cloudJson);
        }

        private SaveData LoadLocalData()
        {
            var localJson = PlayerPrefs.GetString(SaveConstants.PlayerPrefsKey);
            
            return CreateSaveData(localJson);
        }

        private static SaveData CreateSaveData(string cloudJson)
        {
            if (string.IsNullOrEmpty(cloudJson))
                return SaveData.Default;
            
            try
            {
                return JsonConvert.DeserializeObject<SaveData>(cloudJson);
            }
            catch
            {
                return SaveData.Default;
            }
        }
    }
}