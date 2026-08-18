using System;
using Assets.Game.Scripts.Services.CloudSaves;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.Saves
{
    public class SaveService : ISaveService
    {
        private readonly ICloudService _cloudSaveService;
        
        private SaveData _saveData = new();

        public SaveService(ICloudService cloudSaveService) => _cloudSaveService = cloudSaveService;

        public SaveData SaveData => _saveData;

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

        private static SaveData CreateSaveData(string json)
        {
            if (string.IsNullOrEmpty(json))
                return SaveData.Default;
            
            try
            {
                return JsonConvert.DeserializeObject<SaveData>(json);
            }
            catch
            {
                return SaveData.Default;
            }
        }
    }
}