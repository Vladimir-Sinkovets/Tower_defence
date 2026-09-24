using System;

namespace Assets.Game.Scripts.Battle.UI.StartPanel
{
    public interface IStartView
    {
        event Action OnStartButtonClicked;
        void Hide();
    }
}