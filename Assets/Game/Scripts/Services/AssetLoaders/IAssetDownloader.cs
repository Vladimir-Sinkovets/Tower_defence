using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.AssetLoaders
{
    public interface IAssetDownloader
    {
        UniTask<bool> LoadAsync();
    }
}