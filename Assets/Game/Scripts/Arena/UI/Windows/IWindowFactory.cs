namespace Assets.Game.Scripts.Arena.UI.Windows
{
    public interface IWindowFactory
    {
        IWindowPresenter Create(WindowType type);
    }
}