namespace Assets.Game.Scripts.Battle.UI.Windows.EndGamePanel
{
    public interface IWindowsManager
    {
        public void Open(WindowType type);
        public void Close(WindowType type);
        void CloseAll();
    }
}