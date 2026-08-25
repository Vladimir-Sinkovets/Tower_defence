using Photon.Pun;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly ArenaConfig _config;

        public PlayerFactory(ArenaConfig config) => _config = config;

        public Player CreatePlayer()
        {
            var player = PhotonNetwork.Instantiate(_config.PlayerPrefabName, Vector3.zero, Quaternion.identity);

            return player.GetComponent<Player>();
        }
    }
}