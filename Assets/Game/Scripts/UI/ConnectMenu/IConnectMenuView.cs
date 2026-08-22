using System;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.UI.ConnectMenu
{
    public interface IConnectMenuView
    {
        event Action OnClosePanelButtonClicked;
        event Action OnOpenPanelButtonClicked;
        event Action OnConnectRoomButtonClicked;
        event Action OnCreateRoomButtonClicked;
        void Show();
        UniTask Hide();
    }
}