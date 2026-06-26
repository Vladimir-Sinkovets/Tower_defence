using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Saves
{
    public class SaveDataLoader : IInitializable
    {
        private readonly ISaveService _saveService;
        
        public SaveDataLoader(ISaveService saveService) => _saveService = saveService;
        
        public void Initialize()
        {
            var json = PlayerPrefs.GetString(SaveConstants.PlayerPrefsKey);

            if (string.IsNullOrEmpty(json))
            {
                _saveService.MetaCurrency = 0;
                _saveService.WavesRecord = 0;
            }
            else
            {
                var saveData = JsonConvert.DeserializeObject<SaveData>(json);
                
                _saveService.MetaCurrency = saveData.MetaCurrency;
                _saveService.WavesRecord = saveData.WavesRecord;
                _saveService.SetUpgrades(saveData.Upgrades);
            }
        }
    }
}