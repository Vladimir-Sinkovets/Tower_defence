using System;
using UnityEngine;

namespace Assets.Game.Scripts.UI.ConnectMenu
{
    public class ConnectMenuPresenter : IDisposable
    {
        private readonly IConnectMenuView _connectMenuView;

        public ConnectMenuPresenter(IConnectMenuView connectMenuView)
        {
            _connectMenuView = connectMenuView;
            
            _connectMenuView.OnClosePanelButtonClicked += OnClosePanelButtonClickedHandler;
            _connectMenuView.OnOpenPanelButtonClicked += OnOpenPanelButtonClickedHandler;
            _connectMenuView.OnConnectRoomButtonClicked += OnConnectRoomButtonClickedHandler;
            _connectMenuView.OnCreateRoomButtonClicked += OnCreateRoomButtonClickedHandler;
        }

        private void OnClosePanelButtonClickedHandler() => _connectMenuView.Hide();
        private void OnOpenPanelButtonClickedHandler() => _connectMenuView.Show();
        
        private void OnConnectRoomButtonClickedHandler() => Connect();
        private void OnCreateRoomButtonClickedHandler() => CreateRoom();

        private void Connect()
        {
            Debug.Log("Connect");
        }

        private void CreateRoom()
        {
            Debug.Log("Create");
        }

        public void Dispose()
        {
            _connectMenuView.OnClosePanelButtonClicked -= OnClosePanelButtonClickedHandler;
            _connectMenuView.OnOpenPanelButtonClicked -= OnOpenPanelButtonClickedHandler;
            _connectMenuView.OnConnectRoomButtonClicked -= OnConnectRoomButtonClickedHandler;
            _connectMenuView.OnCreateRoomButtonClicked -= OnCreateRoomButtonClickedHandler;
        }
    }
}