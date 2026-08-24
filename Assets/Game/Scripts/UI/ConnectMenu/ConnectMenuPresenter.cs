using System;
using Assets.Game.Scripts.Services.Net;

namespace Assets.Game.Scripts.UI.ConnectMenu
{
    public class ConnectMenuPresenter : IDisposable
    {
        private readonly IConnectMenuView _connectMenuView;
        private readonly INetworkService _networkService;

        public ConnectMenuPresenter(IConnectMenuView connectMenuView, INetworkService networkService)
        {
            _connectMenuView = connectMenuView;
            _networkService = networkService;

            _connectMenuView.OnClosePanelButtonClicked += OnClosePanelButtonClickedHandler;
            _connectMenuView.OnOpenPanelButtonClicked += OnOpenPanelButtonClickedHandler;
            _connectMenuView.OnConnectRoomButtonClicked += OnConnectRoomButtonClickedHandler;
            _connectMenuView.OnCreateRoomButtonClicked += OnCreateRoomButtonClickedHandler;

            _networkService.OnRoomCreated += OnRoomCreatedHandler;
            _networkService.OnRoomJoined += OnRoomJoinedHandler;
            _networkService.OnError += OnErrorHandler;
        }

        private void OnErrorHandler(string message)
        {
            _connectMenuView.Unlock();
            _connectMenuView.HideConnectingPanel();
            
            _connectMenuView.ShowErrorMessage(message);
        }

        private void OnRoomJoinedHandler(string _)
        {
            _connectMenuView.Unlock();
            _connectMenuView.HideConnectingPanel();
        }

        private void OnRoomCreatedHandler(string _)
        {
            _connectMenuView.Unlock();
            _connectMenuView.HideConnectingPanel();
        }

        private void OnClosePanelButtonClickedHandler() => _connectMenuView.Hide();
        private void OnOpenPanelButtonClickedHandler() => _connectMenuView.Show();
        
        private void OnConnectRoomButtonClickedHandler() => Connect();
        private void OnCreateRoomButtonClickedHandler() => CreateRoom();

        private void Connect()
        {
            _connectMenuView.Block();
            _connectMenuView.ShowConnectingPanel();
            
            var id = _connectMenuView.RoomId;
            
            _networkService.ConnectRoom(id);
        }

        private void CreateRoom()
        {
            _connectMenuView.Block();
            _connectMenuView.ShowConnectingPanel();
            
            _networkService.CreateRoom();
        }

        public void Dispose()
        {
            _connectMenuView.OnClosePanelButtonClicked -= OnClosePanelButtonClickedHandler;
            _connectMenuView.OnOpenPanelButtonClicked -= OnOpenPanelButtonClickedHandler;
            _connectMenuView.OnConnectRoomButtonClicked -= OnConnectRoomButtonClickedHandler;
            _connectMenuView.OnCreateRoomButtonClicked -= OnCreateRoomButtonClickedHandler;

            _networkService.OnRoomCreated -= OnRoomCreatedHandler;
            _networkService.OnRoomJoined -= OnRoomJoinedHandler;
            _networkService.OnError -= OnErrorHandler;
        }
    }
}