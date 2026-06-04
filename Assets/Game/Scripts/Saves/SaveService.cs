using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.Saves
{
    public class SaveService : ISaveService
    {
        private const string PlayerPrefsKey = "SaveData";

        public event Action OnSaved;

        public void Save(SaveData saveData)
        {
            var json = JsonConvert.SerializeObject(saveData);
            
            PlayerPrefs.SetString(PlayerPrefsKey, json);
            
            OnSaved?.Invoke();
        }

        public SaveData GetSaveData()
        {
            var json = PlayerPrefs.GetString(PlayerPrefsKey);

            if (string.IsNullOrEmpty(json))
                return new SaveData()
                {
                    MetaCurrency = 0,
                    WavesRecord = 0,
                    Upgrades = new Dictionary<string, int>(),
                };

            return JsonConvert.DeserializeObject<SaveData>(json);
        }
    }
}