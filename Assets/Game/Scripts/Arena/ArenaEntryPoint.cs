using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Assets.Game.Scripts.Arena.Services.GameOverManager;
using Assets.Game.Scripts.Arena.Services.PlayerAccessors;
using Assets.Game.Scripts.Arena.Services.UIFactories;
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
        private readonly IUIFactory _iuiFactory;
        private readonly IGameOverManager _gameOverManager;
        private readonly IPlayerAccessor _playerAccessor;

        public ArenaEntryPoint(IPlayerFactory playerFactory,
            IPlayerController playerController,
            CinemachineCamera cineMachineCamera,
            IEnemySpawner enemySpawner,
            IUIFactory iuiFactory,
            IGameOverManager gameOverManager,
            IPlayerAccessor playerAccessor)
        {
            _playerFactory = playerFactory;
            _playerController = playerController;
            _cineMachineCamera = cineMachineCamera;
            _enemySpawner = enemySpawner;
            _iuiFactory = iuiFactory;
            _gameOverManager = gameOverManager;
            _playerAccessor = playerAccessor;
        }
        
        public void Initialize() => InitializeAsync().Forget();

        private async UniTaskVoid InitializeAsync()
        {
            await UniTask.NextFrame();
            
            var player = _playerFactory.CreatePlayer();

            if (player.PhotonView.IsMine)
            {
                _cineMachineCamera.Follow = player.transform;
                
                _iuiFactory.CreateHUD(player.Health);
                
                var input = _iuiFactory.CreateArenaInput();
                
                _playerController.Init(input.Joystick, player);
                
                _gameOverManager.Init(player.Health);

                player.ShootingBuilding.Init(player);

                _playerAccessor.SetCurrentPlayer(player);
            }
            
            _enemySpawner.Init();
        }
    }
}