using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Saves
{
    public class SaveDataLoader : IInitializable
    {
        private readonly SaveData _saveData;
        
        public SaveDataLoader(SaveData saveData) => _saveData = saveData;
        
        public void Initialize()
        {
            var json = PlayerPrefs.GetString(SaveConstants.PlayerPrefsKey);

            if (string.IsNullOrEmpty(json))
            {
                _saveData.MetaCurrency = 0;
                _saveData.WavesRecord = 0;
                _saveData.Upgrades.Clear();
            }
            else
            {
                var saveData = JsonConvert.DeserializeObject<SaveData>(json);
                
                _saveData.MetaCurrency = saveData.MetaCurrency;
                _saveData.WavesRecord = saveData.WavesRecord;
                _saveData.Upgrades = saveData.Upgrades;
            }
        }
    }
}