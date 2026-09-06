using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Game.Scripts.Services.Net
{
    public class NetworkService : INetworkService
    {
        public bool IsConnected => PhotonNetwork.IsConnected;
        public bool IsInRoom => PhotonNetwork.InRoom;

        public event Action OnConnected;
        public event Action<string> OnRoomJoined;
        public event Action<string> OnRoomCreated;
        public event Action<string> OnError;

        private readonly PhotonCallbacks _callbacks;

        private string _roomId;
        private bool _createRoomRequested;

        public NetworkService(PhotonCallbacks callbacks)
        {
            _callbacks = callbacks;

            _callbacks.ConnectedToMaster += OnConnectedToMaster;
            _callbacks.JoinedRoom += OnJoinedRoom;
            _callbacks.CreatedRoom += OnCreatedRoom;
            _callbacks.JoinRoomFailed += OnJoinRoomFailed;
            _callbacks.CreateRoomFailed += OnCreateRoomFailed;
            _callbacks.Disconnected += OnDisconnected;

            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public void ConnectRoom(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                OnError?.Invoke("Room ID is empty.");
                return;
            }

            _roomId = id.Trim();
            _createRoomRequested = false;

            ConnectToPhoton();
        }

        public void CreateRoom()
        {
            _roomId = null;
            _createRoomRequested = true;

            ConnectToPhoton();
        }
        
        public void LoadScene(string sceneName)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            
            PhotonNetwork.LoadLevel(sceneName);
        }

        private void ConnectToPhoton()
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                OnConnectedToMaster();
                return;
            }

            if (PhotonNetwork.IsConnected)
                return;

            PhotonNetwork.ConnectUsingSettings();
        }

        private void OnConnectedToMaster()
        {
            OnConnected?.Invoke();

            if (_createRoomRequested)
            {
                CreateRoomInternal();
                return;
            }

            if (!string.IsNullOrEmpty(_roomId))
            {
                JoinRoomInternal(_roomId);
            }
        }

        private void JoinRoomInternal(string roomId)
        {
            PhotonNetwork.JoinRoom(roomId);
        }

        private void CreateRoomInternal()
        {
            var roomId = GenerateRoomId();

            var roomOptions = new RoomOptions
            {
                MaxPlayers = 4,
                IsVisible = true,
                IsOpen = true
            };

            PhotonNetwork.CreateRoom(roomId, roomOptions);
        }

        private void OnJoinedRoom()
        {
            var roomId = PhotonNetwork.CurrentRoom.Name;

            ClearPendingRequest();

            OnRoomJoined?.Invoke(roomId);
        }

        private void OnCreatedRoom()
        {
            var roomId = PhotonNetwork.CurrentRoom.Name;

            ClearPendingRequest();
            
            Debug.Log($"Created room: {roomId}");

            OnRoomCreated?.Invoke(roomId);
        }

        private void OnJoinRoomFailed(string message)
        {
            ClearPendingRequest();

            OnError?.Invoke($"Failed to join room. Message: {message}");
        }

        private void OnCreateRoomFailed(string message)
        {
            ClearPendingRequest();

            OnError?.Invoke($"Failed to create room. Message: {message}");
        }

        private void OnDisconnected(DisconnectCause cause) => OnError?.Invoke($"Disconnected from Photon: {cause}");

        private void ClearPendingRequest()
        {
            _roomId = null;
            _createRoomRequested = false;
        }

        private static string GenerateRoomId()
        {
            return "11"; // todo: fix
            // return Guid.NewGuid()
            //     .ToString("N")
            //     .Substring(0, 6)
            //     .ToUpperInvariant();
        }

        public void Dispose()
        {
            _callbacks.ConnectedToMaster -= OnConnectedToMaster;
            _callbacks.JoinedRoom -= OnJoinedRoom;
            _callbacks.CreatedRoom -= OnCreatedRoom;
            _callbacks.JoinRoomFailed -= OnJoinRoomFailed;
            _callbacks.CreateRoomFailed -= OnCreateRoomFailed;
            _callbacks.Disconnected -= OnDisconnected;
        }
    }
}