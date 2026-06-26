using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.AssetProviders;
using Assets.Game.Scripts.Services.FirebaseSetups;
using Assets.Game.Scripts.Upgrades;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private UpgradeConfigs _upgradeConfigs;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SaveService>().AsSingle();
            
            Container.BindInterfacesTo<SaveDataLoader>().AsSingle();
            
            Container.BindInterfacesTo<AddressableAssetProvider>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<FirebaseSetup>().AsSingle();

            Container.BindInstance(_upgradeConfigs).AsSingle();
        }
    }
}