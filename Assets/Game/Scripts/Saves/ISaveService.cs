using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Saves
{
    public interface ISaveService
    {
        SaveData SaveData { get; }
        void Save();
        UniTask LoadAsync();
    }
}