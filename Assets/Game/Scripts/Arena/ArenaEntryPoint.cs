using Assets.Game.Scripts.Arena.Services.ArenaContexts;
using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Assets.Game.Scripts.Arena.Services.GameOverManager;
using Assets.Game.Scripts.Arena.Services.HudFactories;
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
        private readonly IPlayerAccessor _playerAccessor;
        private readonly IHudFactory _hudFactory;
        private readonly IGameOverManager _gameOverManager;

        public ArenaEntryPoint(IPlayerFactory playerFactory,
            IPlayerController playerController,
            CinemachineCamera cineMachineCamera,
            IEnemySpawner enemySpawner,
            IPlayerAccessor playerAccessor,
            IHudFactory hudFactory,
            IGameOverManager gameOverManager)
        {
            _playerFactory = playerFactory;
            _playerController = playerController;
            _cineMachineCamera = cineMachineCamera;
            _enemySpawner = enemySpawner;
            _playerAccessor = playerAccessor;
            _hudFactory = hudFactory;
            _gameOverManager = gameOverManager;
        }
        
        public void Initialize() => InitializeAsync().Forget();

        private async UniTaskVoid InitializeAsync()
        {
            await UniTask.NextFrame();
            
            var player = _playerFactory.CreatePlayer();

            if (player.PhotonView.IsMine)
            {
                _playerController.Init(player);
                
                _cineMachineCamera.Follow = player.transform;
                
                _hudFactory.CreateHUD(player.Health);
                
                _gameOverManager.Init(player.Health);
            }
            
            _playerAccessor.UpdatePlayers();
            
            _enemySpawner.Init();
        }
    }
}