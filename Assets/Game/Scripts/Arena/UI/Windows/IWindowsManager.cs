namespace Assets.Game.Scripts.Arena.UI.Windows
{
    public interface IWindowsManager
    {
        public void Open(WindowType type);
        public void Close(WindowType type);
        void CloseAll();
    }
}