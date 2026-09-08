using System;

namespace Assets.Game.Scripts.Services.Net
{
    public interface INetworkService
    {
        bool IsConnected { get; }
        bool IsInRoom { get; }
        event Action OnConnected;
        event Action<string> OnRoomJoined;
        event Action<string> OnRoomCreated;
        event Action<string> OnError;
        void ConnectRoom(string id);
        void CreateRoom();
        void LoadScene(string sceneName);
        void DisconnectRoom();
    }
}