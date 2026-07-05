using UnityEngine;

namespace Assets.Game.Scripts.Services.Ads
{
    [CreateAssetMenu(fileName = "AdsConfig", menuName = "Ads")]
    public class AdsConfig : ScriptableObject
    {
        public string AndroidGameId;
        public string IOSGameId;
        public string AndroidRewardAdId = "Rewarded_Android";
        public string IOSRewardAdId = "Rewarded_iOS";
        public string AndroidInterstitialAdId = "Interstitial_Android";
        public string IOSInterstitialAdId = "Interstitial_iOS";
        public bool TestMode = true;
    }
}