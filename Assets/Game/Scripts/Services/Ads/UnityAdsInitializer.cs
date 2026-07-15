using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace Assets.Game.Scripts.Services.Ads
{
    public class UnityAdsInitializer : IInitializable, IUnityAdsInitializationListener
    {
        private readonly AdsConfig _config;
        
        private string _gameId;

        public UnityAdsInitializer(AdsConfig config) => _config = config;

        public void Initialize()
        {
#if UNITY_IOS
            _gameId = _config.IOSGameId;
#elif UNITY_ANDROID
            _gameId = _config.AndroidGameId;
#elif UNITY_EDITOR
            _gameId = _config.AndroidGameId;
#endif

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _config.TestMode, this);
            }
        }

        public void OnInitializationComplete() => Debug.Log("Unity Ads initialization complete.");

        public void OnInitializationFailed(UnityAdsInitializationError error, string message) => Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}