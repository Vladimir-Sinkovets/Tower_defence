using Assets.Game.Scripts.Services.AssetLoaders;
using Assets.Game.Scripts.Services.StartScreens;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class StartGameInstaller : MonoInstaller
    {
        [SerializeField] private StartScreen _startScreen;
        
        public override void InstallBindings()
        {
            Container.Bind<IStartScreen>().FromInstance(_startScreen).AsSingle();
            
            Container.BindInterfacesTo<AssetDownloader>().AsSingle();
            Container.BindInterfacesTo<Bootstrap>().AsSingle();
        }
    }
}