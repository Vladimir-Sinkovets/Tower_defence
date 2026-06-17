using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Game.Scripts.Services.AssetProviders
{
    public class AddressableAssetProvider : IAssetProvider, IDisposable
    {
        private readonly Dictionary<AssetReference, AsyncOperationHandle> _handles = new();

        public async UniTask<T> Load<T>(AssetReference assetReference, CancellationToken cancellationToken = default) where T : class
        {
            if (_handles.TryGetValue(assetReference, out var existingHandle))
            {
                if (!existingHandle.IsDone)
                    await existingHandle.ToUniTask(cancellationToken: cancellationToken);
                
                return existingHandle.Result as T;
            }

            var handle = Addressables.LoadAssetAsync<T>(assetReference);

            _handles.Add(assetReference, handle);

            await handle.ToUniTask(cancellationToken: cancellationToken);

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new Exception($"Failed to load addressable: {assetReference.RuntimeKey}");

            return handle.Result;
        }

        public void Unload(AssetReference assetReference)
        {
            if (!_handles.TryGetValue(assetReference, out var handle))
                return;

            Addressables.Release(handle);

            _handles.Remove(assetReference);
        }

        public void UnloadAll()
        {
            foreach (var handle in _handles.Values)
                Addressables.Release(handle);

            _handles.Clear();
        }

        public void Dispose() => UnloadAll();
    }
}