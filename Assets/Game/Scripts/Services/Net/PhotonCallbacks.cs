using System;
using Photon.Pun;
using Photon.Realtime;

namespace Assets.Game.Scripts.Services.Net
{
    public class PhotonCallbacks : MonoBehaviourPunCallbacks
    {
        public event Action ConnectedToMaster;
        public event Action JoinedRoom;
        public event Action CreatedRoom;
        public event Action<string> JoinRoomFailed;
        public event Action<string> CreateRoomFailed;
        public event Action<DisconnectCause> Disconnected;
        public event Action<Player> MasterClientSwitched;
        
        public override void OnConnectedToMaster() => ConnectedToMaster?.Invoke();
        public override void OnJoinedRoom() => JoinedRoom?.Invoke();
        public override void OnCreatedRoom() => CreatedRoom?.Invoke();
        public override void OnJoinRoomFailed(short returnCode, string message) => JoinRoomFailed?.Invoke(message);
        public override void OnCreateRoomFailed(short returnCode, string message) => CreateRoomFailed?.Invoke(message);
        public override void OnDisconnected(DisconnectCause cause) => Disconnected?.Invoke(cause);
        public override void OnMasterClientSwitched(Player newMasterClient) => MasterClientSwitched?.Invoke(newMasterClient);
    }
}