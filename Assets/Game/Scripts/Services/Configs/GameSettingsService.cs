using System;
using Assets.Game.Scripts.Upgrades;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services.Configs
{
    public class GameSettingsService : IInitializable
    {
        private readonly UpgradeConfigs _upgradesConfig;
        private const string GameConfigKey = "Game_config";
        
        public GameSettings GameSettings { get; private set; }

        public GameSettingsService(UpgradeConfigs upgradesConfig) => _upgradesConfig = upgradesConfig;

        public void Initialize() => FetchRemoteConfig();

        private void FetchRemoteConfig()
        {
            TimeSpan cacheTime = TimeSpan.Zero; 

            FirebaseRemoteConfig.DefaultInstance.FetchAsync(cacheTime)
                .ContinueWithOnMainThread(fetchTask => {
                    if (fetchTask.IsCompleted) {
                        FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                            .ContinueWithOnMainThread(activateTask => {
                                if (activateTask.IsCompleted) {
                                    Debug.Log("Configuration updated");
                                    SetConfig();
                                }
                            });
                    } else {
                        Debug.LogError("Configuration update error");
                    }
                });
        }

        private void SetConfig()
        {
            var dict = FirebaseRemoteConfig.DefaultInstance.AllValues;

            var json = dict[GameConfigKey].StringValue;
            
            GameSettings = JsonConvert.DeserializeObject<GameSettings>(json);
            
            GameSettings.UpgradesSettings.CastleAttackSpeedUpgradeSettings.Icon = _upgradesConfig.CastleAttackSpeedUpgradeIcon;
            GameSettings.UpgradesSettings.CastleDamageUpgradeSettings.Icon = _upgradesConfig.CastleDamageUpgradeIcon;
            GameSettings.UpgradesSettings.CastleHpUpgradeSettings.Icon = _upgradesConfig.CastleHpUpgradeIcon;
            GameSettings.UpgradesSettings.TowerAttackSpeedUpgradeSettings.Icon = _upgradesConfig.TowerAttackSpeedUpgradeIcon;
            GameSettings.UpgradesSettings.TowerDamageUpgradeSettings.Icon = _upgradesConfig.TowerDamageUpgradeIcon;
        }
    }
}