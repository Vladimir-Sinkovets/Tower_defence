using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.CloudSaves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class Bootstrap : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
        private ICloudService _cloudService;
        private ISaveService _saveService;
        private IGameSettingLoader _gameSettingLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader, IGameSettingLoader gameSettingLoader, ICloudService cloudService, ISaveService saveService)
        {
            _sceneLoader = sceneLoader;
            _gameSettingLoader = gameSettingLoader;
            _cloudService = cloudService;
            _saveService = saveService;
        }
        
        private void Start() => LoadAsync().Forget();

        private async UniTask LoadAsync()
        {
            await _gameSettingLoader.FetchRemoteConfigAsync();

            await _cloudService.Initialize();

            await _saveService.LoadAsync();
            
            _sceneLoader.LoadScene(SceneNames.Menu);
        }
    }
}