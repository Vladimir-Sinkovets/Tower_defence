using Assets.Game.Scripts.Arena.Services.PlayerControllers;
using Assets.Game.Scripts.Arena.Services.PlayerFactory;
using Unity.Cinemachine;
using Zenject;

namespace Assets.Game.Scripts.Arena
{
    public class ArenaEntryPoint : IInitializable
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly IPlayerController _playerController;
        private readonly CinemachineCamera _cineMachineCamera;

        public ArenaEntryPoint(IPlayerFactory playerFactory, IPlayerController playerController, CinemachineCamera cineMachineCamera)
        {
            _playerFactory = playerFactory;
            _playerController = playerController;
            _cineMachineCamera = cineMachineCamera;
        }
        
        public void Initialize()
        {
            var player = _playerFactory.CreatePlayer();

            if (player.PhotonView.IsMine)
            {
                _playerController.Init(player);
                _cineMachineCamera.Follow = player.transform;
            }
        }
    }
}