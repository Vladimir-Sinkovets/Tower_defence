using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Assets.Game.Scripts.Services.Ads
{
    public class MainMenuInterstitialAdsManager : IInitializable, IDisposable
    {
        private static int _adsCallCount;
        
        private readonly IInterstitialAdsService _interstitialAdsService;
        
        private CancellationTokenSource _cancellationTokenSource;

        public MainMenuInterstitialAdsManager(IInterstitialAdsService interstitialAdsService) => _interstitialAdsService = interstitialAdsService;

        public void Initialize() => ShowAd();

        private async UniTask ShowAd()
        {
            _adsCallCount++;
            
            if (_adsCallCount % 2 != 0)
                return;
            
            _cancellationTokenSource = new CancellationTokenSource();
            
            await _interstitialAdsService.LoadAdAsync(_cancellationTokenSource.Token);
            
            await _interstitialAdsService.ShowAdAsync(_cancellationTokenSource.Token);
        }


        public void Dispose() => _cancellationTokenSource?.Cancel();
    }
}