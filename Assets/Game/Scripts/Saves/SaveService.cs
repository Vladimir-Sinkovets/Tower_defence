using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Saves
{
    public class SaveService : ISaveService, IInitializable
    {
        private const string PlayerPrefsKey = "SaveData";

        public event Action OnSaved;

        private SaveData _saveData;

        public void Initialize()
        {
            var json = PlayerPrefs.GetString(PlayerPrefsKey);

            if (string.IsNullOrEmpty(json))
            {
                _saveData = new SaveData()
                {
                    MetaCurrency = 0,
                    WavesRecord = 0,
                    Upgrades = new Dictionary<string, int>(),
                };
            }
            else
            {
                _saveData = JsonConvert.DeserializeObject<SaveData>(json);
            }
        }

        public void Save(SaveData saveData)
        {
            var json = JsonConvert.SerializeObject(saveData);
            
            PlayerPrefs.SetString(PlayerPrefsKey, json);
            
            OnSaved?.Invoke();
        }

        public SaveData GetSaveData() => _saveData;
    }
}