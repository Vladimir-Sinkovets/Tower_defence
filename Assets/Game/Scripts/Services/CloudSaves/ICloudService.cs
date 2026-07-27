using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.CloudSaves
{
    public interface ICloudService
    {
        UniTask Initialize();
        UniTask SaveAsync(string data);
        UniTask<string> LoadAsync();
    }
}