using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.AssetLoaders;
using Assets.Game.Scripts.Services.CloudSaves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Services.StartScreens;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class Bootstrap : MonoBehaviour
    {
        private const string DownloadingAssetsErrorMessage = "Downloading assets error";
        private const string LoadingMessage = "Loading...";

        private ISceneLoader _sceneLoader;
        private ICloudService _cloudService;
        private ISaveService _saveService;
        private IGameSettingLoader _gameSettingLoader;
        private IAssetDownloader _assetDownloader;
        private IStartScreen _startScreen;
        
        [Inject]
        private void Construct(
            ISceneLoader sceneLoader,
            IGameSettingLoader gameSettingLoader,
            ICloudService cloudService,
            ISaveService saveService,
            IAssetDownloader assetDownloader,
            IStartScreen startScreen)
        {
            _sceneLoader = sceneLoader;
            _gameSettingLoader = gameSettingLoader;
            _cloudService = cloudService;
            _saveService = saveService;
            _assetDownloader = assetDownloader;
            _startScreen = startScreen;
        }
        
        private void Start() => LoadAsync().Forget();

        private async UniTask LoadAsync()
        {
            await _gameSettingLoader.FetchRemoteConfigAsync();
            
            _startScreen.ShowMessage(LoadingMessage);
            
            if (!await _assetDownloader.LoadAsync())
            {
                _startScreen.ShowMessage(DownloadingAssetsErrorMessage);

                return;
            }

            await _cloudService.Initialize();

            await _saveService.LoadAsync();
            
            _sceneLoader.LoadScene(SceneNames.Menu);
        }
    }
}