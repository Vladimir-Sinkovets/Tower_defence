using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Assets.Game.Scripts.Arena.Services.PlayerControllers;
using Assets.Game.Scripts.Arena.Services.PlayerFactory;
using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using Zenject;

namespace Assets.Game.Scripts.Arena
{
    public class ArenaEntryPoint : IInitializable
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly IPlayerController _playerController;
        private readonly CinemachineCamera _cineMachineCamera;
        private readonly IEnemySpawner _enemySpawner;

        public ArenaEntryPoint(IPlayerFactory playerFactory, IPlayerController playerController, CinemachineCamera cineMachineCamera, IEnemySpawner enemySpawner)
        {
            _playerFactory = playerFactory;
            _playerController = playerController;
            _cineMachineCamera = cineMachineCamera;
            _enemySpawner = enemySpawner;
        }
        
        public void Initialize() => InitializeAsync().Forget();

        private async UniTaskVoid InitializeAsync()
        {
            await UniTask.Yield();
            
            var player = _playerFactory.CreatePlayer();

            if (player.PhotonView.IsMine)
            {
                _playerController.Init(player);
                _cineMachineCamera.Follow = player.transform;
            }
            
            _enemySpawner.Init();
        }
    }
}