using System;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.Services.Configs
{
    public class GameSettingsService : IGameSettingsAccessor, IGameSettingLoader
    {
        private const string GameConfigKey = "Game_config";

        public GameSettings Settings { get; private set; }
        
        public async UniTask FetchRemoteConfigAsync()
        {
            var dependencyStatus = await FirebaseApp.CheckDependenciesAsync();
            
            if (dependencyStatus != DependencyStatus.Available)
            {
                Debug.LogError($"[{nameof(GameSettingsService)}] Firebase dependencies not available: {dependencyStatus}");
                return;
            }

            try
            {
                await FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero).AsUniTask();
                
                await FirebaseRemoteConfig.DefaultInstance.ActivateAsync().AsUniTask();
                
                var json = FirebaseRemoteConfig.DefaultInstance.AllValues[GameConfigKey].StringValue;
            
                Settings = JsonConvert.DeserializeObject<GameSettings>(json);
                
                Debug.Log($"[{nameof(GameSettingsService)}] Configuration updated");
            }
            catch
            {
                Debug.LogError($"[{nameof(GameSettingsService)}] Configuration update error");
            }
        }
    }
}