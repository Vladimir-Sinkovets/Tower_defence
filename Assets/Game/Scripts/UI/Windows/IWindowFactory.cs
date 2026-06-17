using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.UI.Windows
{
    public interface IWindowFactory
    {
        UniTask<IWindowPresenter> Create(WindowType type);
    }
}