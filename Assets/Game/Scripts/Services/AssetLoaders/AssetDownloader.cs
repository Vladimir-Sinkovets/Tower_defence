using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Game.Scripts.Services.AssetLoaders
{
    public class AssetDownloader : IAssetDownloader
    {
        private const string Label = "default";
        
        public async UniTask<bool> LoadAsync()
        {
            try
            {
                await Addressables.InitializeAsync();

                var sizeHandle = Addressables.GetDownloadSizeAsync(Label);
                var size = await sizeHandle.ToUniTask();
                Addressables.Release(sizeHandle);

                if (size > 0)
                {
                    var downloadHandle = Addressables.DownloadDependenciesAsync(Label);

                    await downloadHandle.ToUniTask();

                    if (downloadHandle.Status != AsyncOperationStatus.Succeeded)
                    {
                        Debug.LogException(downloadHandle.OperationException);
                        Addressables.Release(downloadHandle);
                        return false;
                    }

                    Addressables.Release(downloadHandle);
                }

                Debug.Log($"[{nameof(AssetDownloader)})] All assets ready");

                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }
    }
}