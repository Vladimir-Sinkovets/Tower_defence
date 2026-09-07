using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly ArenaConfig _config;
        private readonly DiContainer _container;

        public PlayerFactory(ArenaConfig config, DiContainer container)
        {
            _config = config;
            _container = container;
        }

        public ArenaPlayer CreatePlayer()
        {
            var playerGameObject = PhotonNetwork.Instantiate(_config.PlayerPrefabName, Vector3.zero, Quaternion.identity);
            
            _container.InjectGameObject(playerGameObject);

            var player = playerGameObject.GetComponent<ArenaPlayer>();
            
            player.Init(_config.Hp);
            
            return player;
        }
    }
}