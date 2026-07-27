using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.Core;
using UnityEngine;

namespace Assets.Game.Scripts.Services.CloudSaves
{
    public class UnityCloudSaveService : ICloudService
    {
        private const string SaveKey = "data";
        
        public async UniTask Initialize()
        {
            await UnityServices.InitializeAsync().AsUniTask();
            await AuthenticationService.Instance.SignInAnonymouslyAsync().AsUniTask();
            
            Debug.Log($"[{nameof(UnityCloudSaveService)}] Sign in anonymously");
        }

        public async UniTask SaveAsync(string data)
        {
            try
            {
                await CloudSaveService.Instance.Data.Player.SaveAsync(new Dictionary<string, SaveItem>()
                {
                    { SaveKey , new SaveItem(data, string.Empty)}
                })
                    .AsUniTask();

                Debug.Log($"[{nameof(UnityCloudSaveService)}] Saved to unity cloud successfully");
            }
            catch (Exception e)
            {
                Debug.LogError($"[{nameof(UnityCloudSaveService)}] Save failed: {e.Message}");
            }
        }

        public async UniTask<string> LoadAsync()
        {
            var data = await  CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string>() { SaveKey })
                .AsUniTask();

            return data[SaveKey].Value.GetAsString();
        }
    }
}