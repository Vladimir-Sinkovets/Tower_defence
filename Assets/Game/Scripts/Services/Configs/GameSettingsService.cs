using System;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services.Configs
{
    public class GameSettingsService : IInitializable
    {
        private const string GameConfigKey = "Game_config";

        private GameSettings _gameSettings;

        public async UniTask<GameSettings> GetSettingsAsync()
        {
            await UniTask.WaitUntil(() => _gameSettings != null);
            
            return _gameSettings;
        }
        
        public void Initialize() => FetchRemoteConfigAsync();

        private async UniTask FetchRemoteConfigAsync()
        {
            TimeSpan cacheTime = TimeSpan.Zero; 

            await FirebaseRemoteConfig.DefaultInstance.FetchAsync(cacheTime);
            
            var activated = await FirebaseRemoteConfig.DefaultInstance.ActivateAsync();

            if (activated)
            {
                var json = FirebaseRemoteConfig.DefaultInstance.AllValues[GameConfigKey].StringValue;
            
                _gameSettings = JsonConvert.DeserializeObject<GameSettings>(json);
                
                Debug.Log("Configuration updated");
            }
            else
            {
                Debug.LogError("Configuration update error");
            }
        }
    }
}