using Assets.Game.Scripts.Arena.Player;
using Assets.Game.Scripts.Arena.Services.ConstantUpgradeAppliers;
using Photon.Pun;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly ArenaConfig _config;
        private readonly IConstantUpgradeApplier _upgradeApplier;

        public PlayerFactory(ArenaConfig config, IConstantUpgradeApplier upgradeApplier)
        {
            _config = config;
            _upgradeApplier = upgradeApplier;
        }

        public ArenaPlayer CreatePlayer()
        {
            var playerGameObject = PhotonNetwork.Instantiate(_config.PlayerPrefabName, Vector3.zero, Quaternion.identity);

            var player = playerGameObject.GetComponent<ArenaPlayer>();

            player.Init(_upgradeApplier.ApplyHpUpgrade(_config.Hp));
            
            return player;
        }
    }
}