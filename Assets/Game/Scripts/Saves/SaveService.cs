using UnityEngine;

namespace Assets.Game.Scripts.Saves
{
    public class SaveService : ISaveService
    {
        private const string PlayerPrefsKey = "SaveData";
        
        public void Save(SaveData saveData)
        {
            var json = JsonUtility.ToJson(saveData);
            
            PlayerPrefs.SetString(PlayerPrefsKey, json);
        }

        public SaveData GetSaveData()
        {
            var json = PlayerPrefs.GetString(PlayerPrefsKey);

            if (string.IsNullOrEmpty(json))
                return new SaveData()
                {
                    MetaCurrency = 0,
                    WavesRecord = 0,
                };
            
            return JsonUtility.FromJson<SaveData>(json);
        }
    }
}