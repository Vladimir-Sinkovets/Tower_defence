using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.CloudSaves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using Unity.Services.CloudSave;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class StartGameEntryPoint : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        private GameSettingsService _gameSettingsService;
        private ICloudService _cloudService;
        private ISaveService _saveService;

        [Inject]
        private void Construct(SceneLoader sceneLoader, GameSettingsService gameSettingsService, ICloudService cloudService, ISaveService saveService)
        {
            _sceneLoader = sceneLoader;
            _gameSettingsService = gameSettingsService;
            _cloudService = cloudService;
            _saveService = saveService;
        }
        
        private void Start() => LoadAsync().Forget();

        private async UniTask LoadAsync()
        {
            await _gameSettingsService.FetchRemoteConfigAsync();

            await _cloudService.Initialize();

            await _saveService.LoadAsync();
            
            _sceneLoader.LoadScene(SceneNames.Menu);
        }
    }
}