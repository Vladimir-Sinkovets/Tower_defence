using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Assets.Game.Scripts.Services.AssetProviders
{
    public interface IAssetProvider
    {
        UniTask<T> Load<T>(AssetReference assetReference, CancellationToken cancellationToken = default) where T : class;
        void Unload(AssetReference assetReference);
        void UnloadAll();
    }
}