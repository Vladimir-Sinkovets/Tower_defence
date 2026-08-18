using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.AssetLoaders;
using Assets.Game.Scripts.Services.CloudSaves;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Purchases;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Services.StartScreens;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class Bootstrap : IInitializable
    {
        private const string DownloadingAssetsErrorMessage = "Downloading assets error";
        private const string LoadingMessage = "Loading...";

        private readonly ISceneLoader _sceneLoader;
        private readonly ICloudService _cloudService;
        private readonly ISaveService _saveService;
        private readonly IGameSettingLoader _gameSettingLoader;
        private readonly IAssetDownloader _assetDownloader;
        private readonly IStartScreen _startScreen;
        private readonly IInAppPurchaseManager _inAppPurchaseManager;

        private Bootstrap(
            ISceneLoader sceneLoader,
            IGameSettingLoader gameSettingLoader,
            ICloudService cloudService,
            ISaveService saveService,
            IAssetDownloader assetDownloader,
            IStartScreen startScreen,
            IInAppPurchaseManager inAppPurchaseManager)
        {
            _sceneLoader = sceneLoader;
            _gameSettingLoader = gameSettingLoader;
            _cloudService = cloudService;
            _saveService = saveService;
            _assetDownloader = assetDownloader;
            _startScreen = startScreen;
            _inAppPurchaseManager = inAppPurchaseManager;
        }
        
        public void Initialize() => LoadAsync().Forget();

        private async UniTask LoadAsync()
        {
            _startScreen.ShowMessage("Start");

            await _inAppPurchaseManager.InitializeAsync();
            
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