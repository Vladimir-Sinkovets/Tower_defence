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
        private readonly GameDataHolder _gameDataHolder;

        public SaveService(ICloudService cloudSaveService, GameDataHolder gameDataHolder)
        {
            _cloudSaveService = cloudSaveService;
            _gameDataHolder = gameDataHolder;
        }

        public void Save()
        {
            _gameDataHolder.Data.LastSaveDate = DateTime.UtcNow;
            
            var dataToSave = _gameDataHolder.Data.ToRecord();
            
            var json = JsonConvert.SerializeObject(dataToSave);
            
            _cloudSaveService.SaveAsync(json);
            
            PlayerPrefs.SetString(SaveConstants.PlayerPrefsKey, json);
        }

        public async UniTask LoadAsync()
        {
            var localData = LoadLocalData();
            var cloudData = await LoadCloudDataAsync();

            if (localData.LastSaveDate > cloudData.LastSaveDate)
            {
                _gameDataHolder.Data = localData;
                Debug.Log($"[{nameof(SaveService)}] Loaded local data");
            }
            else
            {
                _gameDataHolder.Data = cloudData;
                Debug.Log($"[{nameof(SaveService)}] Loaded cloud data");
            }
        }

        private async UniTask<GameData> LoadCloudDataAsync()
        {
            var cloudJson = await _cloudSaveService.LoadAsync();

            return CreateGameData(cloudJson);
        }

        private GameData LoadLocalData()
        {
            var localJson = PlayerPrefs.GetString(SaveConstants.PlayerPrefsKey);
            
            return CreateGameData(localJson);
        }

        private static GameData CreateGameData(string json)
        {
            if (string.IsNullOrEmpty(json))
                return GameData.Default;
            
            try
            {
                var dataRecord = JsonConvert.DeserializeObject<GameDataRecord>(json);
                
                return dataRecord.ToDomain();
            }
            catch
            {
                return GameData.Default;
            }
        }
    }
}