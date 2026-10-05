using System;
using System.Threading;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Game.Scripts.Services.Ads
{
    public class MainMenuInterstitialAdsManager : IInitializable, IDisposable
    {
        private int _adsCallCount;
        
        private readonly IInterstitialAdsService _interstitialAdsService;
        private readonly GameDataHolder _gameDataHolder;

        private CancellationTokenSource _cancellationTokenSource;

        public MainMenuInterstitialAdsManager(IInterstitialAdsService interstitialAdsService, GameDataHolder gameDataHolder)
        {
            _interstitialAdsService = interstitialAdsService;
            _gameDataHolder = gameDataHolder;
        }

        public void Initialize() => SceneManager.sceneLoaded += OnSceneLoaded;

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == SceneNames.Menu)
            {
                ShowAd().Forget();
            }
        }

        private async UniTask ShowAd()
        {
            if (_gameDataHolder.Data.IsAdsDisabled)
                return;
            
            _adsCallCount++;
            
            if (_adsCallCount % 2 != 0)
                return;
            
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            
            await _interstitialAdsService.ShowAdAsync(_cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}