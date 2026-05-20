namespace Assets.Game.Scripts.UI.Windows
{
    public interface IWindowsManager
    {
        public void Open(WindowType type);
        public void Close(WindowType type);
    }
}