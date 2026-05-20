namespace Assets.Game.Scripts.UI.Windows
{
    public interface IWindowFactory
    {
        IWindowPresenter Create(WindowType type);
    }
}