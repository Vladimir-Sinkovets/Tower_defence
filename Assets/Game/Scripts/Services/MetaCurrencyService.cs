using UnityEngine;

namespace Assets.Game.Scripts.Services
{
    public class MetaCurrencyService : ScriptableObject
    {
        private const string PrefsKey = "MetaCurrency";

        public int Total => PlayerPrefs.GetInt(PrefsKey, 0);

        public void Add(int amount)
        {
            var value = Total + amount;

            Set(value);
        }

        private void Set(int value)
        {
            PlayerPrefs.SetInt(PrefsKey, value);
            PlayerPrefs.Save();
        }
    }
}