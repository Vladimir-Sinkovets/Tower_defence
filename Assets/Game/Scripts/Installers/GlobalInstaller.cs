using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Ads;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.AssetProviders;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.FirebaseSetups;
using Assets.Game.Scripts.Services.Purchases;
using Assets.Game.Scripts.Services.Purchases.Configs;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Upgrades;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private UpgradeConfigs _upgradeConfigs;
        [SerializeField] private AdsConfig _adsConfig;
        [SerializeField] private InAppPurchasesConfig _inAppPurchasesConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SaveService>().AsSingle();
            
            Container.BindInterfacesTo<SaveDataLoader>().AsSingle();
            
            Container.BindInterfacesTo<AddressableAssetProvider>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<FirebaseSetup>().AsSingle();

            Container.BindInterfacesTo<UnityAdsInitializer>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<MainMenuInterstitialAdsManager>().AsSingle();
            
            Container.BindInterfacesTo<UnityAdsService>().AsSingle();
            
            Container.BindInstance(_adsConfig).AsSingle();

            Container.BindInstance(_upgradeConfigs).AsSingle();

            Container.Bind<SceneLoader>().AsSingle();
            
            Container.BindInterfacesTo<FirebaseAnalyticsProvider>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameSettingsService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<InAppPurchaseManager>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<InAppPurchaseExecutor>().AsSingle();
            
            Container.BindInstance(_inAppPurchasesConfig).AsSingle();
        }
    }
}