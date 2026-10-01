using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Battle.UI.Windows.EndGamePanel
{
    public interface IWindowFactory
    {
        IWindowPresenter Create(WindowType type);
    }
}