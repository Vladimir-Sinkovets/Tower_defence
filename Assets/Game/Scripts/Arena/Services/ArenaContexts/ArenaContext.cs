using System.Collections.Generic;
using Assets.Game.Scripts.Arena.Player;
using Photon.Pun;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.ArenaContexts
{
    public class ArenaContext : MonoBehaviourPunCallbacks, IPlayerAccessor
    {
        [SerializeField] private PhotonView _photonView;
        
        private readonly List<ArenaPlayer> _players = new();
        public IEnumerable<ArenaPlayer> Players => _players;
        
        public void UpdatePlayers() => _photonView.RPC(nameof(FindPlayers), RpcTarget.All);

        [PunRPC]
        private void FindPlayers()
        {
            var players = FindObjectsByType<ArenaPlayer>(FindObjectsSortMode.None);
            
            _players.Clear();
            
            _players.AddRange(players);
        }
    }
}