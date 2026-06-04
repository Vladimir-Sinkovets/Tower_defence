using Assets.Game.Scripts.Saves;
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
            Container.Bind<ISaveService>().To<SaveService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<FirebaseSetup>().AsSingle();

            Container.BindInstance(_upgradeConfigs).AsSingle();
        }
    }
}