using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.UI.Windows
{
    public interface IWindowsManager
    {
        public UniTask Open(WindowType type);
        public void Close(WindowType type);
        void CloseAll();
    }
}