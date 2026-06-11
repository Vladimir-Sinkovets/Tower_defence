using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.Saves
{
    public class SaveService : ISaveService
    {
        private readonly SaveData _saveData;

        public SaveService(SaveData saveData) => _saveData = saveData;

        public void Save()
        {
            var json = JsonConvert.SerializeObject(_saveData);
            
            PlayerPrefs.SetString(SaveConstants.PlayerPrefsKey, json);
        }
    }
}