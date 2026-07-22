using System;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services.Configs
{
    public class GameSettingsService
    {
        private const string GameConfigKey = "Game_config";

        public GameSettings Settings { get; private set; }
        
        public async UniTask FetchRemoteConfigAsync()
        {
            var cacheTime = TimeSpan.Zero;

            await FirebaseRemoteConfig.DefaultInstance.FetchAsync(cacheTime);
            
            var activated = await FirebaseRemoteConfig.DefaultInstance.ActivateAsync();

            if (activated)
            {
                var json = FirebaseRemoteConfig.DefaultInstance.AllValues[GameConfigKey].StringValue;
            
                Settings = JsonConvert.DeserializeObject<GameSettings>(json);
                
                Debug.Log("Configuration updated");
            }
            else
            {
                Debug.LogError("Configuration update error");
            }
        }
    }
}