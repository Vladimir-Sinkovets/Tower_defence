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
        string RoomId { get; }
        void Show();
        UniTask Hide();
        void Block();
        void ShowConnectingPanel();
        void Unlock();
        void HideConnectingPanel();
        void ShowErrorMessage(string message);
    }
}