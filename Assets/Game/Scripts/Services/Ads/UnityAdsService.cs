using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Assets.Game.Scripts.Services.Ads
{
    public class UnityAdsService : IAdsRewardService, IInterstitialAdsService
    {
        private readonly string _adRewardId;
        private readonly string _adInterstitialId;

        public UnityAdsService(AdsConfig config)
        {
#if UNITY_IOS
            _adUnitId = config.IOSAdUnitId;
            _adInterstitialId = config.IOSInterstitialUnitId
#elif UNITY_ANDROID
            _adRewardId = config.AndroidRewardAdId;
            _adInterstitialId = config.AndroidInterstitialAdId;
#else
            _adUnitId = string.Empty;
            _adInterstitialId = string.Empty;
#endif
        }

        async UniTask IAdsRewardService.ShowAdAsync(CancellationToken cancellationToken) => await ShowAdAsync(_adRewardId, cancellationToken);
        async UniTask IAdsRewardService.LoadAdAsync(CancellationToken cancellationToken) => await LoadAdAsync(_adRewardId, cancellationToken);

        async UniTask IInterstitialAdsService.ShowAdAsync(CancellationToken cancellationToken) => await ShowAdAsync(_adInterstitialId, cancellationToken);
        async UniTask IInterstitialAdsService.LoadAdAsync(CancellationToken cancellationToken) => await LoadAdAsync(_adInterstitialId, cancellationToken);
        
        
        private async UniTask LoadAdAsync(string adId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(adId))
                throw new InvalidOperationException("Ad Unit Id is not configured.");

            var tcs = new UniTaskCompletionSource();

            using (cancellationToken.Register(() => tcs.TrySetCanceled()))
            {
                Advertisement.Load(adId, new LoadListener(
                    onLoaded: () =>
                    {
                        Debug.Log($"Unity Ads loaded: {adId}");
                        tcs.TrySetResult();
                    },
                    onFailed: (error, message) =>
                    {
                        tcs.TrySetException(
                            new Exception($"Failed to load ad. Error: {error}. Message: {message}"));
                    }));

                await tcs.Task;
            }
        }

        private async UniTask ShowAdAsync(string adId, CancellationToken cancellationToken = default)
        {
            if (!Advertisement.isInitialized)
                throw new InvalidOperationException("Unity Ads is not initialized.");

            var tcs = new UniTaskCompletionSource();

            using (cancellationToken.Register(() => tcs.TrySetCanceled()))
            {
                Advertisement.Show(adId, new ShowListener(
                    onCompleted: state =>
                    {
                        if (state == UnityAdsShowCompletionState.COMPLETED)
                        {
                            tcs.TrySetResult();
                        }
                        else
                        {
                            tcs.TrySetException(
                                new Exception($"Rewarded ad finished with state: {state}"));
                        }
                    },
                    onFailed: (error, message) =>
                    {
                        tcs.TrySetException(
                            new Exception($"Failed to show ad. Error: {error}. Message: {message}"));
                    }));

                await tcs.Task;
            }
        }

        private sealed class LoadListener : IUnityAdsLoadListener
        {
            private readonly Action _onLoaded;
            private readonly Action<UnityAdsLoadError, string> _onFailed;

            public LoadListener(
                Action onLoaded,
                Action<UnityAdsLoadError, string> onFailed)
            {
                _onLoaded = onLoaded;
                _onFailed = onFailed;
            }

            public void OnUnityAdsAdLoaded(string adUnitId)
            {
                _onLoaded?.Invoke();
            }

            public void OnUnityAdsFailedToLoad(
                string adUnitId,
                UnityAdsLoadError error,
                string message)
            {
                _onFailed?.Invoke(error, message);
            }
        }

        private sealed class ShowListener : IUnityAdsShowListener
        {
            private readonly Action<UnityAdsShowCompletionState> _onCompleted;
            private readonly Action<UnityAdsShowError, string> _onFailed;

            public ShowListener(
                Action<UnityAdsShowCompletionState> onCompleted,
                Action<UnityAdsShowError, string> onFailed)
            {
                _onCompleted = onCompleted;
                _onFailed = onFailed;
            }

            public void OnUnityAdsShowStart(string adUnitId)
            {
            }

            public void OnUnityAdsShowClick(string adUnitId)
            {
            }

            public void OnUnityAdsShowComplete(
                string adUnitId,
                UnityAdsShowCompletionState showCompletionState)
            {
                _onCompleted?.Invoke(showCompletionState);
            }

            public void OnUnityAdsShowFailure(
                string adUnitId,
                UnityAdsShowError error,
                string message)
            {
                _onFailed?.Invoke(error, message);
            }
        }
    }
}