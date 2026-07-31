using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.Configs
{
    public interface IGameSettingLoader
    {
        UniTask FetchRemoteConfigAsync();
    }
}