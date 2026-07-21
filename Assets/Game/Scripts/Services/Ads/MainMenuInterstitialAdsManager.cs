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
        private readonly ISaveService _saveService;

        private CancellationTokenSource _cancellationTokenSource;

        public MainMenuInterstitialAdsManager(IInterstitialAdsService interstitialAdsService, ISaveService saveService)
        {
            _interstitialAdsService = interstitialAdsService;
            _saveService = saveService;
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
            if (_saveService.IsaAdsDisabled)
                return;
            
            _adsCallCount++;
            
            if (_adsCallCount % 2 != 0)
                return;
            
            _cancellationTokenSource = new CancellationTokenSource();
            
            await _interstitialAdsService.ShowAdAsync(_cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            _cancellationTokenSource?.Cancel();
        }
    }
}