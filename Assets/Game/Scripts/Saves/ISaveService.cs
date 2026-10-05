using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Saves
{
    public interface ISaveService
    {
        void Save();
        UniTask LoadAsync();
    }
}